using System.Collections.Generic;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Category;
using CookieBook.Infrastructure.Parameters.Category;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> GetAsync(int id, bool asNoTracking = false);

        Task<IEnumerable<Category>> GetAsync(CategoryParameters parameters, bool asNoTracking = false);

        Task<Category> AddAsync(CreateCategory command);

        Task UpdateAsync(int id, UpdateCategory command);
    }
}