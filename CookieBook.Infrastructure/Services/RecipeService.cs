using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Recipe;
using CookieBook.Infrastructure.Commands.Recipe.Component;
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
                .Include(x => x.User)
                .Include(x => x.Rates)
                .Include(x => x.Components)
                .Include(x => x.RecipeImage)
                .Include(x => x.RecipeCategories).ThenInclude(y => y.Category)
                .SingleOrDefaultAsync();

            if (recipe == null)
                throw new CorruptedOperationException("Invalid recipe id");

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

            recipes = recipes
                .Include(x => x.User)
                .Include(x => x.Rates)
                .Include(x => x.Components)
                .Include(x => x.RecipeImage)
                .Include(x => x.RecipeCategories).ThenInclude(y => y.Category);

            return await recipes.ToListAsync();
        }

        public async Task<Recipe> AddAsync(CreateRecipe command, User user)
        {
            var recipe = new Recipe(command.Name, command.Description, command.IsLactoseFree,
                command.IsGlutenFree, command.IsVegan, command.IsVegetarian);

            var components = CreateComponentsFromCommand(command.Components);

            await _context.Components.AddRangeAsync(components);
            recipe.Components = components;

            await _context.Recipes.AddAsync(recipe);
            user.Recipes.Add(recipe);

            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task UpdateAsync(UpdateRecipe command, int id)
        {
            if (await _context.Recipes.ExistsInDatabaseAsync(id) == false)
                throw new CorruptedOperationException("Recipe doesn't exist.");

            var recipe = await GetAsync(id);
            recipe.Update(command.Name, command.Description, command.IsLactoseFree,
                command.IsGlutenFree, command.IsVegan, command.IsVegetarian);

            _context.Components.RemoveRange(recipe.Components);
            recipe.Components.Clear();

            var components = CreateComponentsFromCommand(command.Components);
            await _context.Components.AddRangeAsync(components);
            recipe.Components = components;

            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
        }

        private List<Component> CreateComponentsFromCommand(List<CreateComponent> command)
        {
            var components = new List<Component>();
            foreach (var component in command)
            {
                components.Add(new Component(component.Name, component.Unit,
                    component.Amount));
            }

            return components;
        }
    }
}