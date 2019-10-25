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

		public async Task<IEnumerable<Category>> GetAsync()
		{
			var categories = await _context.Categories
				.Include(x => x.RecipeCategories)
				.ThenInclude(y => y.Recipe)
				.ToListAsync();

			return categories;
		}

		public async Task<Category> AddAsync(CreateCategory command)
		{
			var category = new Category(command.Name);

			await _context.Categories.AddAsync(category);
			await _context.SaveChangesAsync();

			return category;
		}

		public async Task UpdateAsync(int id, UpdateCategory command)
		{
			if (await _context.Categories.ExistsInDatabaseAsync(id) == false)
				throw new CorruptedOperationException("Category doesn't exist.");

			var category = await _context.Categories.GetById(id).SingleOrDefaultAsync();

			category.Update(command.Name);

			_context.Categories.Update(category);
			await _context.SaveChangesAsync();
		}
	}
}