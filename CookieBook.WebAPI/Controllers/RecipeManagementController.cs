using System;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Parameters.Recipe;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers
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

        [Authorize(Roles = "user")]
        [HttpPost("recipes")]
        public async Task<IActionResult> CreateRecipeAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet("recipes/{id}")]
        public async Task<IActionResult> ReadRecipeAsync(int id, [FromQuery] string fields)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "user")]
        [HttpPut("recipes/{id}")]
        public async Task<IActionResult> UpdateRecipeAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CATEGORIES
        [HttpGet("categories")]
        public async Task<IActionResult> ReadCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "user")]
        [HttpPost("categories")]
        public async Task<IActionResult> CreateCategoryAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet("categories/{id}")]
        public async Task<IActionResult> ReadCategoryAsync()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "user")]
        [HttpPut("categories/{id}")]
        public async Task<IActionResult> UpdateCategoryAsync()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}