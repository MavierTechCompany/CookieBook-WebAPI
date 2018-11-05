using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Data.QueryExtensions;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Extensions.CustomExceptions;
using CookieBook.Infrastructure.Extensions.Security;
using CookieBook.Infrastructure.Extensions.Security.Interface;
using CookieBook.Infrastructure.Parameters.Account;
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
                throw new CorruptedOperationException("User already exists.");

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
                throw new CorruptedOperationException("User doesn't exist.");

            var loginHash = _hashManager.CalculateDataHash(command.Login);
            var emailHash = _hashManager.CalculateDataHash(command.UserEmail);

            var user = await _context.Users.GetById(id).SingleOrDefaultAsync();

            user.Update(loginHash, emailHash);

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePasswordAsync(int id, UpdatePassword command)
        {
            byte[] newPasswordHash;

            var user = await _context.Users.GetById(id).SingleOrDefaultAsync();

            if (user == null)
                throw new CorruptedOperationException("Userr doesn't exist.");

            if (_hashManager.VerifyPasswordHash(command.Password, user.PasswordHash,
                    user.Salt) == false)
                throw new CorruptedOperationException("Invalid credentials.");

            _hashManager.CalculatePasswordHash(command.NewPassword, user.Salt, out newPasswordHash);
            user.UpdatePassword(newPasswordHash);

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
                throw new CorruptedOperationException("Invlid credentials.");
            if (_hashManager.VerifyPasswordHash(command.Password, user.PasswordHash, user.Salt) == false)
                throw new CorruptedOperationException("Invlid credentials.");

            return await _jwtHandler.CreateTokenAsync(user.Id, user.Role);
        }

        public async Task<User> GetAsync(int id)
        {
            var user = await _context.Users.GetById(id).Include(x => x.UserImage).SingleOrDefaultAsync();

            if (user == null)
                throw new CorruptedOperationException("Invalid id");

            return user;
        }

        public async Task<IEnumerable<User>> GetAsync(AccountsParameters parameters)
        {
            var users = _context.Users.AsQueryable();

            // if (!string.IsNullOrEmpty(parameters.Fields))
            // {
            //     var fields = parameters.Fields.Split(',');

            //     if (PropertyManager.PropertiesExists<User>(fields))
            //     {

            //     }
            // }

            if (parameters.RegistrationDate != default(DateTime))
            {
                users = users.Where(x => x.CreatedAt.Date == parameters.RegistrationDate.Date);
            }

            if (!string.IsNullOrEmpty(parameters.Query))
            {
                var nickForQuery = parameters.Query.ToLowerInvariant();

                users = users.Where(x => x.Nick.ToLowerInvariant().Contains(nickForQuery));
            }

            return await users.ToListAsync();
        }
    }
}