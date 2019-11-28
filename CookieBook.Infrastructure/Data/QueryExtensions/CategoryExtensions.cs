using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data.QueryExtensions
{
	public static class CategoryExtensions
	{
		public static IQueryable<Category> GetByName(this IQueryable<Category> value, string name) =>
			value.Where(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
	}
}