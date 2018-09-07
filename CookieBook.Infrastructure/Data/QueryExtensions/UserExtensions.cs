using System;
using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Data.QueryExtensions
{
    public static class UserExtensions
    {
        public static async Task<bool> ExistsInDatabaseAsync(this IQueryable<User> value, string nick, ulong login,
            ulong email) => await value.Where(x => x.Nick == nick || x.Login == login || x.UserEmail == email).AnyAsync();

        public static async Task<bool> ExistsInDatabaseAsync(this IQueryable<User> value, int id) =>
            await value.Where(x => x.Id == id).AnyAsync();

        public static IQueryable<User> GetByLogin(this IQueryable<User> value,
            ulong login) => value.Where(x => x.Login == login);

        public static IQueryable<User> GetByEmail(this IQueryable<User> value,
            ulong email) => value.Where(x => x.UserEmail == email);

        public static IQueryable<User> GetById(this IQueryable<User> value,
            int id) => value.Where(x => x.Id == id);
    }
}