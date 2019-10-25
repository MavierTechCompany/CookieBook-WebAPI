using System.Collections.Generic;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Category;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
		Task<Category> GetAsync(int id);
		Task<IEnumerable<Category>> GetAsync();
		Task<Category> AddAsync(CreateCategory command);
		Task UpdateAsync(int id, UpdateCategory command);
	}
}