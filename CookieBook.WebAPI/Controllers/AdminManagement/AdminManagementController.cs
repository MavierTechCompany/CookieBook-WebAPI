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

        /// <summary>
        /// Returns JWT token for valid admin
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns JWT token that is used to perform certain actions</response>
        [HttpPost("token")]
        [ProducesResponseType(typeof(JwtDto), 200)]
        public async Task<IActionResult> LoginAdminAsync([FromBody] LoginAccount command)
        {
            var token = await _adminService.LoginAsync(command);
            var tokenDto = _mapper.Map<JwtDto>(token);
            return Ok(tokenDto);
        }

        /// <summary>
        /// Creates new admin
        /// </summary>
        /// <param name="command"></param>
        /// <response code="201">Returns newly created admin</response>
        /// <response code="400">Returns information about failed validation</response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(AdminDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateAdminAsync([FromBody] CreateAdmin command)
        {
            var adminTuple = await _adminService.AddAsync(command);
            var adminDto = _mapper.Map<AdminDto>(adminTuple.Admin);
            adminDto.Login = adminTuple.Login;

            return Created($"{Request.Host}{Request.Path}/{adminTuple.Admin.Id}", adminDto);
        }

        /// <summary>
        /// Returns selected admin
        /// </summary>
        /// <param name="id" example="1">Id of admin that you want to get</param>
        /// <param name="fields"></param>
        /// <response code="200">Returns selected admin</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(AdminShortDto), 200)]
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