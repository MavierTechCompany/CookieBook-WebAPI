using System.Collections.Generic;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Category;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Data.QueryExtensions;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Services
{
	public class CategoryService : ICategoryService
	{
		private readonly CookieContext _context;
        
        public CategoryService(CookieContext context)
        {
			_context = context;
		}

		public async Task<Category> GetAsync(int id)
		{
			var category = await _context.Categories.GetById(id)
				.Include(x => x.RecipeCategories).ThenInclude(y => y.Recipe)
				.SingleOrDefaultAsync();

			if (category == null)
				throw new CorruptedOperationException("Invalid category id");

			return category;
		}

		public Task<IEnumerable<Category>> GetAsync()
		{
			throw new System.NotImplementedException();
		}

		public Task<Category> AddAsync(CreateCategory command)
		{
			throw new System.NotImplementedException();
		}

		public Task UpdateAsync(UpdateCategory command)
		{
			throw new System.NotImplementedException();
		}
	}
}