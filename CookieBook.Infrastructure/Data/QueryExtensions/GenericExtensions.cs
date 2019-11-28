using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data.QueryExtensions
{
    public static class GenericExtensions
    {
		public static async Task<bool> ExistsInDatabaseAsync<T>(this IQueryable<T> value, int id)
            where T:Entity
        {
			return await value.Where(x => x.Id == id).AnyAsync();
		}

		public static IQueryable<T> GetById<T>(this IQueryable<T> value, int id)
            where T:Entity
        {
			return value.Where(x => x.Id == id);
		}
    }
}