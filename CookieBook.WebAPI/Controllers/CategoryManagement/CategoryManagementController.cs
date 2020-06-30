using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Category;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Parameters.Category;
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

        public CategoryManagementController(ICategoryService categoryService, IMapper mapper) : base(mapper)
        {
            _categoryService = categoryService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategory command)
        {
            var category = await _categoryService.AddAsync(command);
            var categoryrDto = _mapper.Map<CategoryDto>(category);

            return Created($"{Request.Host}{Request.Path}/{category.Id}", categoryrDto);
        }

        [HttpGet]
        public async Task<IActionResult> ReadCategoriesAsync(CategoryParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Fields) &&
                !PropertyManager.PropertiesExists<Category>(parameters.Fields))
            {
                return BadRequest();
            }

            var categories = await _categoryService.GetAsync(parameters, true);
            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesDto.ShapeData(parameters.Fields));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ReadCategoryAsync(int id, [FromQuery] string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields) &&
                !PropertyManager.PropertiesExists<Category>(fields))
            {
                return BadRequest();
            }

            var category = await _categoryService.GetAsync(id, true);
            var categoryrDto = _mapper.Map<CategoryDto>(category);

            return Ok(categoryrDto.ShapeData(fields));
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAsync(int id, [FromBody] UpdateCategory command)
        {
            await _categoryService.UpdateAsync(id, command);
            return NoContent();
        }
    }
}