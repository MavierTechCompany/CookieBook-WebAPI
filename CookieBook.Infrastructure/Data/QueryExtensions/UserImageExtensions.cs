using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data.QueryExtensions
{
    public static class UserImageExtensions
    {
        public static async Task<bool> ExistsInDatabaseAsync(this IQueryable<UserImage> value,
            string content) => await value.Where(x => x.ImageContent == content).AnyAsync();

        public static IQueryable<UserImage> GetById(this IQueryable<UserImage> value,
            int id) => value.Where(x => x.Id == id);
    }
}