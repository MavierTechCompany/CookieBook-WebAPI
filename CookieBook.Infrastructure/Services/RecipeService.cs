using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Data.QueryExtensions;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
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

        public async Task<Recipe> GetAsync(int id)
        {
            var recipe = await _context.Recipes.GetById(id)
                .Include(x => x.RecipeImage)
                .Include(x => x.User)
                .Include(x => x.RecipeCategories).ThenInclude(y => y.Category)
                .Include(x => x.RecipeComponents).ThenInclude(z => z.Component).SingleOrDefaultAsync();

            if (recipe == null)
                throw new CorruptedOperationException("Invalid id");

            return recipe;
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