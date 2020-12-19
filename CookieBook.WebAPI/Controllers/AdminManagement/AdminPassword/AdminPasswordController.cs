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

        /// <summary>
        /// Updates admin password
        /// </summary>
        /// <param name="id" example="1">Id of the admin that wants to change his/her password</param>
        /// <param name="command"></param>
        /// <response code="204">Returned when the password update is successful</response>
        /// <response code="400">Returned when validation failds or admin is inactive</response>
        /// <response code="403">Returned when the caller / sender wants to update someone else's password</response>
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePassword command)
        {
            if (id != AccountID)
                return Forbid();

            await _adminService.UpdatePasswordAsync(id, command);
            return NoContent();
        }
    }
}