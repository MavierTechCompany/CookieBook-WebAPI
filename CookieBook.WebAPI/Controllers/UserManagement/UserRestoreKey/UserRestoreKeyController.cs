﻿using AutoMapper;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.Services.Interfaces;
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
        private readonly IUserService _userService;

        public UserRestoreKeyController(IMapper mapper, IUserService userService) : base(mapper) => _userService = userService;

        [HttpPost("block")]
        public async Task<IActionResult> BlockUserAsync([FromBody] BlockUser command)
        {
            await _userService.BlockAsync(command);
            return Ok();
        }

        [HttpPost("request-unblock")]
        public async Task<IActionResult> RequestUnblockUserAsync() => throw new NotImplementedException();

        [Authorize("user")]
        [HttpPost("{id}/restore-key")]
        public async Task<IActionResult> GenerateRestoreKeyAsync(int id) => throw new NotImplementedException();
    }
}