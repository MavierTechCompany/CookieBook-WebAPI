using AutoMapper;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Controllers.AdminManagement
{
    [Route("admin-management/admins")]
    public class AdminManagementController : ApiControllerBase
    {
        public AdminManagementController(IMapper mapper) : base(mapper)
        {
        }

        [HttpPost("token")]
        public async Task<IActionResult> LoginAdminAsync() => throw new NotImplementedException();

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAdminAsync() => throw new NotImplementedException();

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateAdminAsync() => throw new NotImplementedException();
    }
}