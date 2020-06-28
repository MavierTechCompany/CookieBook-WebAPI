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

        public async Task<Rate> GetAsync(int id, bool asNoTracking = false)
        {
            var query = _context.Rates.Where(x => x.Id == id).AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            var rate = await query.SingleOrDefaultAsync();

            if (rate == null)
                throw new CorruptedOperationException("Invalid rate id");

            return rate;
        }

        public async Task<IEnumerable<Rate>> GetByRecipeIdAsync(int recipeId, RatesParameters parameters, bool asNoTracking = false)
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

            if (asNoTracking)
                rates = rates.AsNoTracking();

            return await rates.ToListAsync();
        }
    }
}