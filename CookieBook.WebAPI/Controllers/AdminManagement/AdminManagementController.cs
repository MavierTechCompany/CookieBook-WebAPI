using AutoMapper;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Commands.Admin;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.DTO.Admin;
using CookieBook.Infrastructure.DTO.Base;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
            var adminTuple = await _adminService.AddAsync(command);
            var adminDto = _mapper.Map<AdminDto>(adminTuple.Admin);
            adminDto.Login = adminTuple.Login;

            return Created($"{Request.Host}{Request.Path}/{adminTuple.Admin.Id}", adminDto);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> ReadAdminAsync(int id, [FromQuery] string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields) &&
                !PropertyManager.PropertiesExists<AdminShortDto>(fields))
            {
                return BadRequest();
            }

            var admin = await _adminService.GetAsync(id, true);
            var adminDto = _mapper.Map<AdminShortDto>(admin);

            return Ok(adminDto.ShapeData(fields));
        }
    }
}