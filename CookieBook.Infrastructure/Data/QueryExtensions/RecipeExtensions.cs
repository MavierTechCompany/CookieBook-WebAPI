using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data.QueryExtensions
{
    public static class RecipeExtensions
    {
        public static IQueryable<Recipe> GetByName(this IQueryable<Recipe> value, string name) =>
            value.Where(x => x.Name.ToLowerInvariant() == name.ToLowerInvariant());
    }
}