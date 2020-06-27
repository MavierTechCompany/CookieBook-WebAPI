using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Recipe;
using CookieBook.Infrastructure.Commands.Recipe.Rate;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using CookieBook.Infrastructure.Parameters.Recipe.Rate;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services
{
    public class RateService : IRateService
    {
        private readonly CookieContext _context;

        public RateService(CookieContext context)
        {
            _context = context;
        }

        public async Task<Rate> CreateAsync(CreateRate command, Recipe recipe)
        {
            var rate = new Rate(command.Value, command.Description);
            await _context.Rates.AddAsync(rate);
            recipe.Rates.Add(rate);

            await _context.SaveChangesAsync();

            return rate;
        }

        public async Task<Rate> GetAsync(int id)
        {
            var rate = await _context.Rates.SingleOrDefaultAsync(x => x.Id == id);

            if (rate == null)
                throw new CorruptedOperationException("Invalid rate id");

            return rate;
        }

        public async Task<IEnumerable<Rate>> GetByRecipeIdAsync(int recipeId, RatesParameters parameters)
        {
            var rates = _context.Rates.Where(x => x.RecipeId == recipeId).AsQueryable();

            if (parameters.CreatedAt != default(DateTime))
            {
                rates = rates.Where(x => x.CreatedAt.Date == parameters.CreatedAt.Date);
            }

            if (!string.IsNullOrEmpty(parameters.Query))
            {
                var descriptionFromQuery = parameters.Query.ToLowerInvariant();

                rates = rates.Where(x => x.Description.ToLowerInvariant().Contains(descriptionFromQuery));
            }

            if (parameters.Value != null)
            {
                rates = rates.Where(x => x.Value == parameters.Value);
            }

            return await rates.ToListAsync();
        }
    }
}