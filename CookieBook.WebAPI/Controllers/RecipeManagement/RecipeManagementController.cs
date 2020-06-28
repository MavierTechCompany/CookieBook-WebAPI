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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CookieBook.WebAPI.Controllers.RecipeManagement
{
    [Route("recipe-management")]
    public class RecipeManagementController : ApiControllerBase
    {
        private readonly IRecipeService _recipeService;
        private readonly IRateService _rateService;
        private readonly IMapper _mapper;

        public RecipeManagementController(IRecipeService recipeService, IRateService rateService, IMapper mapper)
        {
            _recipeService = recipeService;
            _rateService = rateService;
            _mapper = mapper;
        }

        [HttpGet("recipes")]
        public async Task<IActionResult> ReadRecipesAsync(RecipesParameters parameters)
        {
            if (!string.IsNullOrWhiteSpace(parameters.Fields) && !PropertyManager.PropertiesExists<Recipe>(parameters.Fields))
            {
                return BadRequest();
            }

            var recipes = await _recipeService.GetAsync(parameters, true);
            var recipesDto = _mapper.Map<IEnumerable<RecipeDto>>(recipes);

            return Ok(recipesDto.ShapeData(parameters.Fields));
        }

        [HttpGet("recipes/{id}")]
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

        [Authorize(Roles = "user")]
        [HttpPost("recipes/{id}/rates")]
        public async Task<IActionResult> CreateRateAsync(int id, [FromBody] CreateRate command)
        {
            var recipe = await _recipeService.GetAsync(id);
            var rate = await _rateService.CreateAsync(command, recipe);
            var rateDto = _mapper.Map<RateDto>(rate);

            return Created($"{Request.Host}{Request.Path}/{rate.Id}", rateDto);
        }

        [HttpGet("recipes/{id}/rates")]
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

        [HttpGet("recipes/{id}/rates/{rateId}")]
        public async Task<IActionResult> ReadRateAsync(int rateId, [FromQuery] string fields)
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