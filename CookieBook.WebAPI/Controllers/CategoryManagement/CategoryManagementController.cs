using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Category;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Parameters.Category;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.Infrastructure.Validators.Category;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.CategoryManagement
{
    [Produces("application/json")]
    [Route("category-management/categories")]
    public class CategoryManagementController : ApiControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryManagementController(ICategoryService categoryService, IMapper mapper) : base(mapper)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Creates new category
        /// </summary>
        /// <param name="command"></param>
        /// <returns>Newly category</returns>
        /// <response code="201">Returns the newly created category</response>
        /// <response code="400">
        /// Returns information about failed validation
        ///
        ///     {
        ///        "key": "0e7ad584-7788-4ab1-95a6-ca0a5b444cbb",
        ///        "name": "Item1",
        ///        "isComplete": true
        ///     }
        /// </response>

        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CategoryDto), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategory command)
        {
            var category = await _categoryService.AddAsync(command);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Created($"{Request.Host}{Request.Path}/{category.Id}", categoryDto);
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

        /// <summary>
        /// Reads the category with specified id.
        /// </summary>
        /// <param name="id" example="1"></param>
        /// <param name="fields" example="Id,Name"></param>
        /// <returns></returns>
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