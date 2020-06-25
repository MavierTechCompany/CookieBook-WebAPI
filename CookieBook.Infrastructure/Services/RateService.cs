using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
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

        public Task<Rate> CreateAsync() => throw new NotImplementedException();

        public Task<Rate> GetAsync(int id) => throw new NotImplementedException();

        public Task<IEnumerable<Rate>> GetByRecipeIdAsync(int recipeId) => throw new NotImplementedException();
    }
}