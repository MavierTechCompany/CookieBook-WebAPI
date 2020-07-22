using AutoMapper;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Controllers.UserManagement.UserRestoreKey
{
    [Route("user-management/users/")]
    public class UserRestoreKeyController : ApiControllerBase
    {
        public UserRestoreKeyController(IMapper mapper) : base(mapper)
        {
        }

        [HttpPost("block")]
        public async Task<IActionResult> BlockUserAsync() => throw new NotImplementedException();

        [HttpPost("request-unblock")]
        public async Task<IActionResult> RequestUnblockUserAsync() => throw new NotImplementedException();

        [Authorize("user")]
        [HttpPost("{id}/restore-key")]
        public async Task<IActionResult> GenerateRestoreKeyAsync(int id) => throw new NotImplementedException();
    }
}