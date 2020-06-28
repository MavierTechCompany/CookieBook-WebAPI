using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Category;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Data.QueryExtensions;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using CookieBook.Infrastructure.Parameters.Category;
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

        public async Task<Category> GetAsync(int id, bool asNoTracking = false)
        {
            var query = _context.Categories.GetById(id)
                .Include(x => x.RecipeCategories).ThenInclude(y => y.Recipe).AsQueryable();

            if (asNoTracking)
                query = query.AsNoTracking();

            var category = await query.SingleOrDefaultAsync();

            if (category == null)
                throw new CorruptedOperationException("Invalid category id");

            return category;
        }

        public async Task<IEnumerable<Category>> GetAsync(CategoryParameters parameters, bool asNoTracking = false)
        {
            var categories = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(parameters.Query))
            {
                var nameForQuery = parameters.Query.ToLowerInvariant();

                categories = categories.Where(x => x.Name.ToLowerInvariant().Contains(nameForQuery));
            }

            categories = categories
                 .Include(x => x.RecipeCategories)
                 .ThenInclude(y => y.Recipe);

            if (asNoTracking)
                categories = categories.AsNoTracking();

            return await categories.ToListAsync();
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