using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly CookieContext _context;

        public AccountService(CookieContext context)
        {
            _context = context;
        }

        public Task<bool> IsActive(int id) => _context.Accounts.Where(x => x.Id == id).Select(x => x.IsActive).SingleOrDefaultAsync();
    }
}