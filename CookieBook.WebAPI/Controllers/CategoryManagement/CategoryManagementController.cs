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
    /// <summary>
    /// CRUD operations for Category
    /// </summary>
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
        /// <response code="201">Returns the newly created category</response>
        /// <response code="400">Returns information about failed validation</response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(typeof(CategoryDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateCategoryAsync([FromBody] CreateCategory command)
        {
            var category = await _categoryService.AddAsync(command);
            var categoryDto = _mapper.Map<CategoryDto>(category);

            return Created($"{Request.Host}{Request.Path}/{category.Id}", categoryDto);
        }

        /// <summary>
        /// Returns collection of categories
        /// </summary>
        /// <param name="parameters"></param>
        /// <response code="200">Returns collection of categories that matched given criteria. May be empty if no item matching the search criteria could be found.</response>
        /// <response code="400">Returned when parameter <b>Fields</b> contains name of a field that isn't a part of the <b>Category</b> object.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), 200)]
        [ProducesResponseType(400)]
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
        /// Returns category with given ID
        /// </summary>
        /// <param name="id" example="1">Category ID</param>
        /// <param name="fields" example="Name,Id,CreatedAt">Names of categry's fields that caller wants to get. Must contains names that are a part of the category object. Names must be separated by a comma</param>
        /// <response code="200">Returns data on the selected category or a subset of it, as specified in the fields.</response>
        /// <response code="400">Returned when parameter <b>Fields</b> contains name of a field that isn't a part of the <b>Category</b> object.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(400)]
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

        /// <summary>
        /// Updates category with given ID
        /// </summary>
        /// <param name="id" example="1">Category ID</param>
        /// <param name="command"></param>
        /// <response code="204">Returned when the update is successful</response>
        /// <response code="400">Returns information about failed validation</response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> UpdateCategoryAsync(int id, [FromBody] UpdateCategory command)
        {
            await _categoryService.UpdateAsync(id, command);
            return NoContent();
        }
    }
}