using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Parameters.Recipe;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly CookieContext _context;

        public RecipeService(CookieContext context)
        {
            _context = context;
        }

        public Task<Recipe> GetAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Recipe>> GetAsync(RecipesParameters parameters)
        {
            var recipes = _context.Recipes.AsQueryable();

            if (parameters.CreatedAt != default(DateTime))
            {
                recipes = recipes.Where(x => x.CreatedAt.Date == parameters.CreatedAt.Date);
            }

            if (!string.IsNullOrEmpty(parameters.Query))
            {
                var nameForQuery = parameters.Query.ToLowerInvariant();

                recipes = recipes.Where(x => x.Name.ToLowerInvariant().Contains(nameForQuery));
            }

            if (parameters.IsLactoseFree != null)
            {
                recipes = recipes.Where(x => x.IsLactoseFree == parameters.IsLactoseFree);
            }

            if (parameters.IsGlutenFree != null)
            {
                recipes = recipes.Where(x => x.IsGlutenFree == parameters.IsGlutenFree);
            }

            if (parameters.IsVegan != null)
            {
                recipes = recipes.Where(x => x.IsVegan == parameters.IsVegan);
            }

            if (parameters.IsVegetarian != null)
            {
                recipes = recipes.Where(x => x.IsVegetarian == parameters.IsVegetarian);
            }

            return await recipes.ToListAsync();
        }
    }
}