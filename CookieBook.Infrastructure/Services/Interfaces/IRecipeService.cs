using System.Collections.Generic;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Recipe;
using CookieBook.Infrastructure.Parameters.Recipe;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<Recipe> GetAsync(int id, bool asNoTracking = false);

        Task<IEnumerable<Recipe>> GetAsync(RecipesParameters parameters, bool asNoTracking = false);

        Task<Recipe> AddAsync(CreateRecipe command, User user);

        Task UpdateAsync(UpdateRecipe command, int id);
    }
}