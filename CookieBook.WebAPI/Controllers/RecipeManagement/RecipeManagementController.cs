using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Recipe.Rate;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Extensions;
using CookieBook.Infrastructure.Parameters.Recipe;
using CookieBook.Infrastructure.Parameters.Recipe.Rate;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using CookieBook.WebAPI.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.RecipeManagement
{
    [Route("recipe-management")]
    public class RecipeManagementController : ApiControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IRateService _rateService;

        public RecipeManagementController(IRecipeService recipeService, IRateService rateService, IMapper mapper) : base(mapper)
        {
            _recipeService = recipeService;
            _rateService = rateService;
        }

        /// <summary>
        /// Returns collection of recipes
        /// </summary>
        /// <param name="parameters"></param>
        /// <response code="200">Returns collection of recipes that matched given criteria. May be empty if no item matching the search criteria could be found.</response>
        /// <response code="400">Returned when parameter <b>Fields</b> contains name of a field that isn't a part of the <b>Recipe</b> object.</response>
        [HttpGet("recipes")]
        [ProducesResponseType(typeof(IEnumerable<RecipeDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ReadRecipesAsync(RecipesParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Fields) && !PropertyManager.PropertiesExists<RecipeDto>(parameters.Fields))
            {
                return BadRequest();
            }

            var recipes = await _recipeService.GetAsync(parameters, true);
            var recipesDto = _mapper.Map<IEnumerable<RecipeDto>>(recipes);

            return Ok(recipesDto.ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Returns recipe with given ID
        /// </summary>
        /// <param name="id" example="2"></param>
        /// <param name="fields" example="Name,Id,CreatedAt"></param>
        /// <response code="200">Returns a specific recipe based on the given id.</response>
        /// <response code="400">Returned when parameter <b>Fields</b> contains name of a field that isn't a part of the <b>Recipe</b> object.</response>
        [HttpGet("recipes/{id}")]
        [ProducesResponseType(typeof(RecipeDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ReadRecipeAsync(int id, [FromQuery] string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields) && !PropertyManager.PropertiesExists<Recipe>(fields))
            {
                return BadRequest();
            }

            var recipe = await _recipeService.GetAsync(id, true);
            var recipeDto = _mapper.Map<RecipeDto>(recipe);

            return Ok(recipeDto.ShapeData(fields));
        }

        /// <summary>
        /// Creates new rate for a specific recipe
        /// </summary>
        /// <param name="id" example="2"></param>
        /// <param name="command"></param>
        /// <response code="201">Returns the newly created rate.</response>
        /// <response code="400">Returns information about failed validation.</response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action.</response>
        [Authorize(Roles = "user")]
        [HttpPost("recipes/{id}/rates")]
        [ProducesResponseType(typeof(RateDto), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> CreateRateAsync(int id, [FromBody] CreateRate command)
        {
            var recipe = await _recipeService.GetAsync(id);
            var rate = await _rateService.CreateAsync(command, recipe);
            var rateDto = _mapper.Map<RateDto>(rate);

            return Created($"{Request.Host}{Request.Path}/{rate.Id}", rateDto);
        }

        /// <summary>
        /// Returns collection of rates for recipe with given ID
        /// </summary>
        /// <param name="id" example="2"></param>
        /// <param name="fields" example="Name,Id,CreatedAt"></param>
        /// <response code="200">Returns a collection of rates that belongs to recipe with given id.</response>
        /// <response code="400">Returned when parameter <b>Fields</b> contains name of a field that isn't a part of the <b>Rate</b> object.</response>
        [HttpGet("recipes/{id}/rates")]
        [ProducesResponseType(typeof(IEnumerable<RateDto>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ReadRatesAsync(int id, RatesParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Fields) && !PropertyManager.PropertiesExists<Rate>(parameters.Fields))
            {
                return BadRequest();
            }

            var rates = await _rateService.GetByRecipeIdAsync(id, parameters, true);
            var ratesDto = _mapper.Map<IEnumerable<RateDto>>(rates);

            return Ok(ratesDto.ShapeData(parameters.Fields));
        }

        /// <summary>
        /// Returns rate with given ID, that belongs to specyfic recipe
        /// </summary>
        /// <param name="id" example="2">ID of the recipe</param>
        /// <param name="rateId" example="5">ID of the rate</param>
        /// <param name="fields" example="Name,Id,CreatedAt">Names of fields you want to shape recipe with</param>
        /// <response code="200">Returns rate with given ID that belongs to specyfic recipe.</response>
        /// <response code="400">Returned when parameter <b>Fields</b> contains name of a field that isn't a part of the <b>Rate</b> object.</response>
        [HttpGet("recipes/{id}/rates/{rateId}")]
        [ProducesResponseType(typeof(RateDto), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> ReadRateAsync(int id, int rateId, [FromQuery] string fields)
        {
            if (!string.IsNullOrWhiteSpace(fields) && !PropertyManager.PropertiesExists<Rate>(fields))
            {
                return BadRequest();
            }

            var rate = await _rateService.GetAsync(rateId, true);
            var rateDto = _mapper.Map<RateDto>(rate);

            return Ok(rateDto.ShapeData(fields));
        }
    }
}