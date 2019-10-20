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
		private readonly IMapper _mapper;

        public UserImageController(IUserService userService, IUserImageService userImageService, IMapper mapper)
        {
			_userService = userService;
			_userImageService = userImageService;
			_mapper = mapper;
		}

		[Authorize(Roles = "user")]
		[HttpPost]
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

		[HttpGet]
		public async Task<IActionResult> GetImageAsync(int id, [FromQuery] string fields)
		{
			if (!string.IsNullOrWhiteSpace(fields) &&
				!PropertyManager.PropertiesExists<Domain.Models.UserImage>(fields))
			{
				return BadRequest();
			}

			var image = await _userImageService.GetByUserIdAsync(id);
			var imageDto = _mapper.Map<UserImageDto>(image);

			return Ok(imageDto.ShapeData(fields));
		}

		[Authorize(Roles = "user")]
		[HttpPut]
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