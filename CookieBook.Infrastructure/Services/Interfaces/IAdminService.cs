using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Admin;
using CookieBook.Infrastructure.Commands.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IAdminService
    {
        Task<(Admin Admin, string Login)> AddAsync(CreateAdmin command);

        Task<string> LoginAsync(LoginAccount command);

        Task<Admin> GetAsync(int id, bool asNoTracking = false);

        Task UpdatePasswordAsync(int id, UpdatePassword command);
    }
}