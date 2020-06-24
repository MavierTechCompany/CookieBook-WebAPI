using CookieBook.Domain.Models;
using CookieBook.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IRateService
    {
        Task<Rate> CreateAsync();

        Task<Rate> GetAsync(int id);

        Task<IEnumerable<Rate>> GetByRecipeIdAsync(int recipeId);
    }
}