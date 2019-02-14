using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data.QueryExtensions
{
    public static class RecipeExtensions
    {
        public static async Task<bool> ExistsInDatabaseAsync(this IQueryable<Recipe> value, int id) =>
            await value.Where(x => x.Id == id).AnyAsync();

        public static IQueryable<Recipe> GetByName(this IQueryable<Recipe> value, string name) =>
            value.Where(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());

        public static IQueryable<Recipe> GetById(this IQueryable<Recipe> value, int id) =>
            value.Where(x => x.Id == id);
    }
}