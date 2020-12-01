using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Infrastructure.Commands.Picture;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.UserManagement.UserImage
{
    [Route("user-management/users/{id}/image")]
    public class UserImageController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserImageService _userImageService;

        public UserImageController(IUserService userService, IUserImageService userImageService, IMapper mapper) : base(mapper)
        {
            _userService = userService;
            _userImageService = userImageService;
        }

        /// <summary>
        /// Creates new image for a specific user. User can create image / aatar only for himself / herself.
        /// </summary>
        /// <param name="id" example="1">Id of the user that creates the image</param>
        /// <param name="command"></param>
        /// <response code="201">Returns the newly created image.</response>
        /// <response code="400">Returns information about failed validation.</response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action.</response>
        /// <response code="403">Returned when caller/sender wants to perform this action for someone else than himself/herself.</response>
        [Authorize(Roles = "user")]
        [HttpPost]
        [ProducesResponseType(typeof(UserImageDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> CreateImageAsync(int id, [FromBody] CreateImage command)
        {
            if (id != AccountID)
                return Forbid();
            if (await _userImageService.ExistsForUser(id) == true)
                return BadRequest("Image already exists.");

            var user = await _userService.GetAsync(id);
            var image = await _userImageService.AddAsync(command, user);

            var imageDto = _mapper.Map<UserImageDto>(image);

            return Created($"{Request.Host}{Request.Path}/{user.Id}", imageDto);
        }

        /// <summary>
        /// Returns rate with given ID, that belongs to specyfic recipe
        /// </summary>
        /// <param name="id" example="1">Id of the user that this image belongs to</param>
        /// <param name="fields" example="Id,CreatedAt">Names of fields you want to shape image with</param>
        /// <response code="200">Returns image that belongs to specyfic user.</response>
        /// <response code="400">Returned when parameter <b>Fields</b> contains name of a field that isn't a part of the <b>Image</b> object.</response>
        [HttpGet]
        [ProducesResponseType(typeof(UserImageDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ReadImageAsync(int id, [FromQuery] string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields) &&
                !PropertyManager.PropertiesExists<Domain.Models.UserImage>(fields))
            {
                return BadRequest();
            }

            var image = await _userImageService.GetByUserIdAsync(id, true);
            var imageDto = _mapper.Map<UserImageDto>(image);

            return Ok(imageDto.ShapeData(fields));
        }

        /// <summary>
        /// Updates image of the user with given ID
        /// </summary>
        /// <param name="id" example="1">Id of the user that this image belongs to</param>
        /// <param name="command"></param>
        /// <response code="204">Returned when the update is successful.</response>
        /// <response code="400">Returns information about failed validation.</response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action.</response>
        /// <response code="403">Returned when caller/sender wants to perform this action for someone else than himself/herself.</response>
        [Authorize(Roles = "user")]
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public async Task<IActionResult> UpdateImageAsync(int id, [FromBody] UpdateImage command)
        {
            if (id != AccountID)
                return Forbid();
            if (await _userImageService.ExistsForUser(id) == false)
                return BadRequest("Image doesn't exist.");

            var user = await _userService.GetAsync(id);
            await _userImageService.UpdateAsync(command, user);
            return NoContent();
        }
    }
}