using System;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Parameters.Account;
using CookieBook.Infrastructure.Parameters.Recipe;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CookieBook.WebAPI.Controllers
{
    [Route("user-management")]
    public class UserManagementController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserImageService _userImageService;

        public UserManagementController(IUserService userService, IUserImageService userImageService)
        {
            _userService = userService;
            _userImageService = userImageService;
        }

        #region USERS
        [HttpPost("users")]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUser command)
        {
            var user = await _userService.AddAsync(command);
            return Created($"/users/{user.Id}", user);
        }

        [HttpGet("users")]
        public async Task<IActionResult> ReadUsersAsync(AccountsParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Fields) &&
                !PropertyManager.PropertiesExists<User>(parameters.Fields))
            {
                return BadRequest();
            }

            var users = await _userService.GetAsync(parameters);
            return Ok(users.ShapeData(parameters.Fields));
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> ReadUserAsync(int id, [FromQuery] string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields) &&
                !PropertyManager.PropertiesExists<User>(fields))
            {
                return BadRequest();
            }

            var user = await _userService.GetAsync(id);
            return Ok(user.ShapeData(fields));
        }

        [Authorize(Roles = "user")]
        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserData command)
        {
            if (id != AccountID)
                return Forbid();

            await _userService.UpdateAsync(id, command);
            return NoContent();
        }
        #endregion

        #region USERS/TOKEN
        [HttpPost("users/token")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUser command)
        {
            var token = await _userService.LoginAsync(command);
            return Ok(token);
        }
        #endregion

        #region USERS/id/PASSWORD
        [Authorize(Roles = "user")]
        [HttpPut("users/{id}/password")]
        public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePassword command)
        {
            if (id != AccountID)
                return Forbid();

            await _userService.UpdatePasswordAsync(id, command);
            return NoContent();
        }
        #endregion

        #region USERS/id/IMAGE
        [Authorize(Roles = "user")]
        [HttpPost("users/{id}/image")]
        public async Task<IActionResult> CreateImageAsync(int id, [FromBody] CreateImage command)
        {
            if (id != AccountID)
                return Forbid();
            if (await _userImageService.ExistsForUser(id) == true)
                return BadRequest("Image already exists.");

            var user = await _userService.GetAsync(id);
            var image = await _userImageService.AddAsync(command, user);

            var resoultImage = new
            {
                image.Id,
                image.UserRef,
                image.ImageContent,
                image.CreatedAt,
                image.UpdatedAt
            };

            return Created($"/users/{id}/image/{user.Id}", resoultImage);
        }

        [HttpGet("users/{id}/image")]
        public async Task<IActionResult> GetImageAsync(int id, [FromQuery] string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields) &&
                !PropertyManager.PropertiesExists<UserImage>(fields))
            {
                return BadRequest();
            }

            var image = await _userImageService.GetByUserIdAsync(id);
            return Ok(image.ShapeData(fields));
        }

        [Authorize(Roles = "user")]
        [HttpPut("users/{id}/image")]
        public async Task<IActionResult> UpdateImageAsync(int id, [FromBody] UpdateImage command)
        {
            if (id != AccountID)
                return Forbid();
            if (await _userImageService.ExistsForUser(id) == false)
                return BadRequest("Image doesn't exist.");

            var user = await _userService.GetAsync(id);
            await _userImageService.UpdateAsync(command, user);
            return NoContent();
        }
        #endregion

        #region USERS/id/RECIPES
        [HttpGet("users/{id}/recipes")]
        public async Task<IActionResult> ReadRecipesAsync(int id, [FromQuery] RecipesParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Fields) &&
                !PropertyManager.PropertiesExists<Recipe>(parameters.Fields))
            {
                return BadRequest();
            }

            var recipes = await _userService.GetUserRecipesAsync(id, parameters);
            return Ok(recipes.ShapeData(parameters.Fields));
        }

        [Authorize(Roles = "user")]
        [HttpPost("users/{id}/recipes")]
        public async Task<IActionResult> CreateRecipeAsync(int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("users/{id}/recipes/{recipeId}")]
        public async Task<IActionResult> ReadRecipeAsync(int id, int recipeId, [FromQuery] string fields)
        {

            if (!string.IsNullOrWhiteSpace(fields) &&
                !PropertyManager.PropertiesExists<Recipe>(fields))
            {
                return BadRequest();
            }

            var recipe = await _userService.GetUserRecipeAsync(id, recipeId);
            if (recipe == null)
                return BadRequest();

            return Ok(recipe.ShapeData(fields));
        }
        #endregion
    }
}