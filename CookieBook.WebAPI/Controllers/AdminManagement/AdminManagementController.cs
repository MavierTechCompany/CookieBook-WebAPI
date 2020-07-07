using AutoMapper;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Admin;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.DTO.Base;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Controllers.AdminManagement
{
    [Route("admin-management/admins")]
    public class AdminManagementController : ApiControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminManagementController(IMapper mapper, IAdminService adminService) : base(mapper)
        {
            _adminService = adminService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> LoginAdminAsync([FromBody] LoginAccount command)
        {
            var token = await _adminService.LoginAsync(command);
            return Ok(new { Token = token });
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAdminAsync([FromBody] CreateAdmin command)
        {
            var admin = await _adminService.AddAsync(command);
            var userDto = _mapper.Map<AccountDto>(admin);

            return Created($"{Request.Host}{Request.Path}/{admin.Id}", userDto);
        }
    }
}