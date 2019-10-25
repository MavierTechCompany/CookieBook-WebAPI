using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Infrastructure.Commands.Category;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.CategoryManagement
{
	[Route("category-management/categories")]
    public class CategoryManagementController : ApiControllerBase
    {
		private readonly ICategoryService _categoryService;
		private readonly IMapper _mapper;

        public CategoryManagementController(ICategoryService categoryService, IMapper mapper)
        {
			_categoryService = categoryService;
			_mapper = mapper;
        }

        #region CATEGORIES
        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategory command)
        {
			var category = await _categoryService.AddAsync(command);
			var categoryrDto = _mapper.Map<CategoryDto>(category);

			return Created($"{Request.Host}{Request.Path}/{category.Id}", categoryrDto);
        }
        #endregion

        #region CATEGORY

        #endregion
    }
}