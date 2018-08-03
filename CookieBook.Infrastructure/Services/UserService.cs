using System;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Extensions.Security;
using CookieBook.Infrastructure.Extensions.Security.Interface;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CookieBook.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly CookieContext _context;
        private readonly IDataHashManager _hashManager;

        public UserService(CookieContext context, IDataHashManager hashManager)
        {
            _context = context;
            _hashManager = hashManager;
        }

        public async Task<User> AddUserAsync(CreateUser command)
        {
            byte[] salt, passwordHash;

            var loginHash = _hashManager.CalculateDataHash(command.Login);
            var emailHash = _hashManager.CalculateDataHash(command.UserEmail);

            if (await UserExistsInDatabaseAsync(command.Nick, loginHash, emailHash) == true)
                throw new Exception("User already exists.");

            _hashManager.CalculatePasswordHash(command.Password, out passwordHash, out salt);
            var restoreKey = PasswordGenerator.GenerateRandomPassword();

            var user = new User(command.Nick, loginHash, salt, passwordHash, restoreKey, emailHash);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> UserExistsInDatabaseAsync(string nick, ulong login, ulong email)
        {
            return (await _context.Users.SingleOrDefaultAsync(x => x.Nick == nick || x.Login == login ||
                x.UserEmail == email)) != null;
        }
    }
}