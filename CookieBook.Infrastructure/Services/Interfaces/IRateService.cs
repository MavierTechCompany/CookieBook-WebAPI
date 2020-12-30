using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Recipe.Rate;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Parameters.Recipe.Rate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IRateService
    {
        Task<Rate> CreateAsync(CreateRate command, Recipe recipe);

        Task<Rate> GetAsync(int id, bool asNoTracking = false);

        Task<IEnumerable<Rate>> GetByRecipeIdAsync(int recipeId, RatesParameters parameters, bool asNoTracking = false);

        Task DeleteAsync(int recipeId, int rateId);
    }
}