using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Recipe;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Parameters.Recipe;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using CookieBook.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.UserManagement.UserRecipe
{
    [Route("user-management/users/{id}/recipes")]
    [AccessableByInactiveAccount(false)]
    public class UserRecipeController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;

        public UserRecipeController(IUserService userService, IRecipeService recipeService, IMapper mapper) : base(mapper)
        {
            _userService = userService;
            _recipeService = recipeService;
        }

        /// <summary>
        /// Returns collection of user recipes
        /// </summary>
        /// <param name="id" example="1">Id of the user that wants to get his/her recipes</param>
        /// <param name="parameters"></param>
        /// <response code="200">Returns collection of recipes that belongs to this user</response>
        /// <response code="400">Returned when validation failds or user is inactive</response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        /// <response code="403">Returned when the caller / sender wants to get someone else's recipes</response>
        [HttpGet]
        [Authorize(Roles = "user")]
        [ProducesResponseType(typeof(IEnumerable<RecipeDto>), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> ReadRecipesAsync(int id, [FromQuery] RecipesParameters parameters)
        {
            if (id != AccountID)
                return Forbid();

            if (!string.IsNullOrWhiteSpace(parameters.Fields) &&
                !PropertyManager.PropertiesExists<Recipe>(parameters.Fields))
            {
                return BadRequest();
            }

            var recipes = await _userService.GetUserRecipesAsync(id, parameters, true);
            var recipesDto = _mapper.Map<IEnumerable<RecipeDto>>(recipes);

            return Ok(recipesDto.ShapeData(parameters.Fields));
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<IActionResult> CreateRecipeAsync(int id, [FromBody] CreateRecipe command)
        {
            if (id != AccountID)
                return Forbid();

            var user = await _userService.GetAsync(id);
            var recipe = await _recipeService.AddAsync(command, user);
            var recipeDto = _mapper.Map<RecipeDto>(recipe);

            return Created($"{Request.Host}{Request.Path}/{recipe.Id}", recipeDto);
        }

        [Authorize(Roles = "user")]
        [HttpGet("{recipeId}")]
        public async Task<IActionResult> ReadRecipeAsync(int id, int recipeId, [FromQuery] string fields)
        {
            if (id != AccountID)
                return Forbid();

            if (!string.IsNullOrWhiteSpace(fields) &&
                !PropertyManager.PropertiesExists<Recipe>(fields))
            {
                return BadRequest();
            }

            var recipe = await _userService.GetUserRecipeAsync(id, recipeId, true);
            if (recipe == null)
                return BadRequest();

            var recipeDto = _mapper.Map<RecipeDto>(recipe);

            return Ok(recipeDto.ShapeData(fields));
        }

        /// <summary>
        /// Updates category with given ID
        /// </summary>
        /// <param name="id" example="2">Id of the user that wants to update his/her recipe</param>
        /// <param name="recipeId" example="1">Id of the recipe that user wants to update</param>
        /// <param name="command"></param>
        /// <returns></returns>
        [Authorize(Roles = "user")]
        [HttpPut("{recipeId}")]
        public async Task<IActionResult> UpdateRecipeAsync(int id, int recipeId, [FromBody] UpdateRecipe command)
        {
            if (id != AccountID)
                return Forbid();

            await _recipeService.UpdateAsync(command, recipeId);
            return NoContent();
        }
    }
}