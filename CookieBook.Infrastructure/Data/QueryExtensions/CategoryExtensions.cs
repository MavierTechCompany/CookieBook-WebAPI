using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data.QueryExtensions
{
	public static class CategoryExtensions
	{
		public static async Task<bool> ExistsInDatabaseAsync(this IQueryable<Category> value, int id) =>
			await value.Where(x => x.Id == id).AnyAsync();

		public static IQueryable<Category> GetByName(this IQueryable<Category> value, string name) =>
			value.Where(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());

		public static IQueryable<Category> GetById(this IQueryable<Category> value, int id) =>
			value.Where(x => x.Id == id);
	}
}