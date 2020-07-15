using CookieBook.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Data.QueryExtensions
{
    public static class AdminExtensions
    {
        public static async Task<bool> ExistsInDatabaseAsync(this IQueryable<Admin> value, string nick, ulong login)
            => await value.Where(x => x.Nick == nick || x.Login == login).AnyAsync();

        public static IQueryable<Admin> GetByLogin(this IQueryable<Admin> value, ulong login)
            => value.Where(x => x.Login == login);
    }
}