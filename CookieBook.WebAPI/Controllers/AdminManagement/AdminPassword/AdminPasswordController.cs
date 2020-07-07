using AutoMapper;
using CookieBook.Infrastructure.Commands.Account;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Controllers.AdminManagement.AdminPassword
{
    [Route("admin-management/admins/{id}/password")]
    public class AdminPasswordController : ApiControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminPasswordController(IMapper mapper, IAdminService adminService) : base(mapper)
        {
            _adminService = adminService;
        }

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePassword command)
        {
            if (id != AccountID)
                return Forbid();

            await _adminService.UpdatePasswordAsync(id, command);
            return NoContent();
        }
    }
}