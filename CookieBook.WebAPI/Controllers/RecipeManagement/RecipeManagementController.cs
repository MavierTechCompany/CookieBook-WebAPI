using System;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
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
        public RecipeManagementController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
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
            return Ok(recipes.ShapeData(parameters.Fields));
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