using System.Collections.Generic;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Parameters.Account;
using CookieBook.Infrastructure.Parameters.Recipe;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> AddAsync(CreateUser command);

        Task UpdateAsync(int id, UpdateUserData command);

        Task<string> LoginAsync(LoginAccount command);

        Task BlockAsync(BlockUser command);

        Task UnblockAsync(UnblockUser command);

        Task<User> GetAsync(int id, bool asNoTracking = false);

        Task<IEnumerable<User>> GetAsync(AccountsParameters parameters, bool asNoTracking = false);

        Task UpdatePasswordAsync(int id, UpdatePassword command);

        Task<IEnumerable<Recipe>> GetUserRecipesAsync(int id, RecipesParameters parameters, bool asNoTracking = false);

        Task<Recipe> GetUserRecipeAsync(int id, int recipeId, bool asNoTracking = false);
    }
}