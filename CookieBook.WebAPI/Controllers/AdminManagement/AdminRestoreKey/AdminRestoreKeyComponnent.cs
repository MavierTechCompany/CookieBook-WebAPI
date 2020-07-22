using AutoMapper;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Controllers.AdminManagement.AdminRestoreKey
{
    [Route("admin-management/admins/")]
    public class AdminRestoreKeyComponnent : ApiControllerBase
    {
        public AdminRestoreKeyComponnent(IMapper mapper) : base(mapper)
        {
        }

        [HttpPost("block")]
        public async Task<IActionResult> BlockAdminAsync() => throw new NotImplementedException();

        [HttpPost("request-unblock")]
        public async Task<IActionResult> RequestUnblockAdminAsync() => throw new NotImplementedException();

        [Authorize("admin")]
        [HttpPost("{id}/unblock")]
        public async Task<IActionResult> UnblockSelectedAdminAsync(int id) => throw new NotImplementedException();

        [Authorize("admin")]
        [HttpPost("{id}/restore-key")]
        public async Task<IActionResult> GenerateRestoreKeyAsync(int id) => throw new NotImplementedException();
    }
}