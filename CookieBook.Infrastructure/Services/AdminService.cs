using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Admin;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Data.QueryExtensions;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using CookieBook.Infrastructure.Extensions.Security;
using CookieBook.Infrastructure.Extensions.Security.Interface;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly CookieContext _context;
        private readonly IDataHashManager _hashManager;
        private readonly IJwtHandler _jwtHandler;

        public AdminService(CookieContext context, IDataHashManager hashManager, IJwtHandler jwtHandler)
        {
            _context = context;
            _hashManager = hashManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<Admin> AddAsync(CreateAdmin command)
        {
            var login = _hashManager.CalculateDataHash(RandomStringGenerator.GenerateUnique()).ToString();
            var loginHash = _hashManager.CalculateDataHash(login);

            if (await _context.Admins.ExistsInDatabaseAsync(command.Nick, loginHash) == true)
                throw new CorruptedOperationException("Admin already exists.");

            _hashManager.CalculatePasswordHash(command.Password, out var passwordHash, out var salt);
            var restoreKey = PasswordGenerator.GenerateRandomPassword();

            var admin = new Admin(command.Nick, loginHash, salt, passwordHash, restoreKey);

            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();

            return admin;
        }

        public async Task<Admin> GetAsync(int id, bool asNoTracking = false)
        {
            var query = _context.Admins.GetById(id);

            if (asNoTracking)
                query = query.AsNoTracking();

            var admin = await query.SingleOrDefaultAsync();

            if (admin == null)
                throw new CorruptedOperationException("Invalid admin id");

            return admin;
        }

        public async Task<string> LoginAsync(LoginAccount command)
        {
            var login = _hashManager.CalculateDataHash(command.LoginOrEmail);

            var admin = await _context.Admins.GetByLogin(login)
                .Select(x => new { x.PasswordHash, x.Salt, x.Role, x.Id })
                .AsNoTracking().SingleOrDefaultAsync();

            if (admin == null)
                throw new CorruptedOperationException("Invalid credentials.");

            if (_hashManager.VerifyPasswordHash(command.Password, admin.PasswordHash, admin.Salt) == false)
                throw new CorruptedOperationException("Invalid credentials.");

            return await _jwtHandler.CreateTokenAsync(admin.Id, admin.Role);
        }

        public async Task UpdatePasswordAsync(int id, UpdatePassword command)
        {
            var admin = await _context.Admins.GetById(id).SingleOrDefaultAsync();

            if (admin == null)
                throw new CorruptedOperationException("Userr doesn't exist.");

            if (_hashManager.VerifyPasswordHash(command.Password, admin.PasswordHash, admin.Salt) == false)
                throw new CorruptedOperationException("Invalid credentials.");

            _hashManager.CalculatePasswordHash(command.NewPassword, admin.Salt, out var newPasswordHash);
            admin.UpdatePassword(newPasswordHash);

            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
        }
    }
}