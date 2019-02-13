using System;
using System.Threading.Tasks;
using CookieBook.Infrastructure.Parameters.Recipe;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers
{
    [Route("recipe-management")]
    public class RecipeManagementController : ApiControllerBase
    {
        #region RECIPES
        [HttpGet("recipes")]
        public async Task<IActionResult> ReadRecipesAsync(RecipesParameters parameters)
        {
            throw new NotImplementedException();
        }

        [HttpPost("recipes")]
        public async Task<IActionResult> AddRecipeAsync()
        {
            throw new NotImplementedException();
        }

        [HttpGet("recipes/{id}")]
        public async Task<IActionResult> ReadRecipeAsync(int id, [FromQuery] string fields)
        {
            throw new NotImplementedException();
        }

        [HttpPut("recipes/{id}")]
        public async Task<IActionResult> UpdateRecipeAsync()
        {
            throw new NotImplementedException();
        }
        #endregion

        #region CATEGORIES
        //TODO GET("categories")
        //TODO POST("categories")
        //TODO GET("categories/{id}")
        //TODO PUT("categories/{id}")
        #endregion
    }
}