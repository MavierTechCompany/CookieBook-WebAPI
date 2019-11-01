using System.Collections.Generic;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Recipe;
using CookieBook.Infrastructure.Parameters.Recipe;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<Recipe> GetAsync(int id);
        Task<IEnumerable<Recipe>> GetAsync(RecipesParameters parameters);
        Task<Recipe> AddAsync(CreateRecipe command, User user);
		Task<Recipe> UpdateAsync(UpdateRecipe command, int id);
	}
}