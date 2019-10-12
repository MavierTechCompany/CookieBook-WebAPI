using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Parameters.Recipe;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.RecipeManagement
{
    [Route("recipe-management")]
    public class RecipeManagementController : ApiControllerBase
    {
        private readonly IRecipeService _recipeService;
		private readonly IMapper _mapper;

        public RecipeManagementController(IRecipeService recipeService, IMapper mapper)
        {
            _recipeService = recipeService;
			_mapper = mapper;
        }

        #region RECIPES
        [HttpGet("recipes")]
        public async Task<IActionResult> ReadRecipesAsync(RecipesParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Fields) &&
                !PropertyManager.PropertiesExists<Recipe>(parameters.Fields))
            {
                return BadRequest();
            }

            var recipes = await _recipeService.GetAsync(parameters);
			var recipesDto = _mapper.Map<IEnumerable<RecipeDto>>(recipes);

			return Ok(recipesDto.ShapeData(parameters.Fields));
        }

        [HttpGet("recipes/{id}")]
        public async Task<IActionResult> ReadRecipeAsync(int id, [FromQuery] string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields) &&
                !PropertyManager.PropertiesExists<Recipe>(fields))
            {
                return BadRequest();
            }

            var recipe = await _recipeService.GetAsync(id);
            return Ok(recipe.ShapeData(fields));
        }
        #endregion
    }
}