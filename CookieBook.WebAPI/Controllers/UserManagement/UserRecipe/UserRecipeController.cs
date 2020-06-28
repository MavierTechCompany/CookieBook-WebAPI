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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.UserManagement.UserRecipe
{
    [Route("user-management/users/{id}/recipes")]
    public class UserRecipeController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRecipeService _recipeService;
        private readonly IMapper _mapper;

        public UserRecipeController(IUserService userService, IRecipeService recipeService, IMapper mapper)
        {
            _userService = userService;
            _recipeService = recipeService;
            _mapper = mapper;
        }

        [Authorize(Roles = "user")]
        [HttpGet]
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