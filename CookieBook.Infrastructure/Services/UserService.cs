using System;
using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Data.QueryExtensions;
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
        private readonly IJwtHandler _jwtHandler;

        public UserService(CookieContext context, IDataHashManager hashManager, IJwtHandler jwtHandler)
        {
            _context = context;
            _hashManager = hashManager;
            _jwtHandler = jwtHandler;
        }

        public async Task<User> AddAsync(CreateUser command)
        {
            byte[] salt, passwordHash;

            var loginHash = _hashManager.CalculateDataHash(command.Login);
            var emailHash = _hashManager.CalculateDataHash(command.UserEmail);

            if (await _context.Users.ExistsInDatabaseAsync(command.Nick, loginHash, emailHash) == true)
                throw new Exception("User already exists.");

            _hashManager.CalculatePasswordHash(command.Password, out passwordHash, out salt);
            var restoreKey = PasswordGenerator.GenerateRandomPassword();

            var user = new User(command.Nick, loginHash, salt, passwordHash, restoreKey, emailHash);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task UpdateAsync(int id, UpdateUserData command)
        {
            if (await _context.Users.ExistsInDatabaseAsync(id) == false)
                throw new Exception("User doesn't exist.");

            var loginHash = _hashManager.CalculateDataHash(command.Login);
            var emailHash = _hashManager.CalculateDataHash(command.UserEmail);

            var user = await _context.Users.GetById(id).SingleOrDefaultAsync();

            user.Update(loginHash, emailHash);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> LoginAsync(LoginUser command)
        {
            var loginOrEmailHash = _hashManager.CalculateDataHash(command.LoginOrEmail);

            var user = await _context.Users.GetByEmail(loginOrEmailHash)
                .Select(x => new { x.PasswordHash, x.Salt, x.Role, x.Id })
                .AsNoTracking().SingleOrDefaultAsync();
            if (user == null)
                user = await _context.Users.GetByLogin(loginOrEmailHash)
                .Select(x => new { x.PasswordHash, x.Salt, x.Role, x.Id })
                .AsNoTracking().SingleOrDefaultAsync();

            if (user == null)
                throw new Exception("Invlid credentials.");
            if (_hashManager.VerifyPasswordHash(command.Password, user.PasswordHash, user.Salt) == false)
                throw new Exception("Invlid credentials.");

            return await _jwtHandler.CreateTokenAsync(user.Id, user.Role);
        }
    }
}