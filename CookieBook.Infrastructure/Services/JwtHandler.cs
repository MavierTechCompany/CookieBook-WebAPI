using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CookieBook.Domain.JWT;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CookieBook.Infrastructure.Services
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings _jwtSettings;

        public JwtHandler(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public JwtDto CreateToken(int userId, string role)
        {
            var dateNow = DateTime.UtcNow;
            var claims = new Claim[]
            {
                new Claim("Id", userId.ToString()),
                new Claim("Role", role),
                new Claim(JwtRegisteredClaimNames.Iat, dateNow.Ticks.ToString()),
            };

            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_jwtSettings.Key)), SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(issuer: _jwtSettings.Issuer,
                claims: claims,
                notBefore: dateNow,
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
            };
        }
    }
}