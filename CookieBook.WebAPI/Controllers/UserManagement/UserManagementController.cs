using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Auth;
using CookieBook.Infrastructure.Commands.User;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.DTO.Base;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Parameters.Account;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using CookieBook.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CookieBook.WebAPI.Controllers.UserManagement
{
    [Route("user-management/users")]
    public class UserManagementController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserManagementController(IUserService userService, IMapper mapper) : base(mapper)
        {
            _userService = userService;
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="command"></param>
        /// <response code="201">Returns newly created user</response>
        /// <response code="400">Returns information about failed validation</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUser command)
        {
            var user = await _userService.AddAsync(command);
            var userDto = _mapper.Map<UserDto>(user);

            return Created($"{Request.Host}{Request.Path}/{user.Id}", userDto);
        }

        /// <summary>
        /// Returns collection of users
        /// </summary>
        /// <param name="parameters"></param>
        /// <response code="200">Returns collection of users that matched given criteria. May be empty if no item matching the search criteria could be found.</response>
        /// <response code="400">Returned when parameter <b>Fields</b> contains name of a field that isn't a part of the <b>User</b> object.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ReadUsersAsync(AccountsParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Fields) &&
                !PropertyManager.PropertiesExists<UserDto>(parameters.Fields))
            {
                return BadRequest();
            }

            var users = await _userService.GetAsync(parameters, true);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersDto.ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Returns user with given ID
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="fields" example="Nick,Id,CreatedAt"></param>
        /// <response code="200">Returns a specific user based on the given id.</response>
        /// <response code="400">Returned when parameter <b>Fields</b> contains name of a field that isn't a part of the <b>User</b> object.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ReadUserAsync(int id, [FromQuery] string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields) &&
                !PropertyManager.PropertiesExists<UserDto>(fields))
            {
                return BadRequest();
            }

            var user = await _userService.GetAsync(id, true);
            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto.ShapeData(fields));
        }

        /// <summary>
        /// Updates user
        /// </summary>
        /// <param name="id" example="1">Id of the user that wants to change his/her data</param>
        /// <param name="command"></param>
        /// <response code="204">Returned when update is successful</response>
        /// <response code="400">Returned when validation failds or user is inactive</response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        /// <response code="403">Returned when the caller / sender wants to update someone else's data</response>
        [Authorize(Roles = "user")]
        [HttpPut("{id}")]
        [AccessableByInactiveAccount(false)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateUserData command)
        {
            if (id != AccountID)
                return Forbid();

            await _userService.UpdateAsync(id, command);
            return NoContent();
        }

        /// <summary>
        /// Returns JWT token for valid user
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns JWT token that is used to perform certain actions</response>
        [HttpPost("token")]
        [ProducesResponseType(typeof(JwtDto), 200)]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginAccount command)
        {
            var token = await _userService.LoginAsync(command);
            var tokenDto = _mapper.Map<JwtDto>(token);
            return Ok(tokenDto);
        }
    }
}