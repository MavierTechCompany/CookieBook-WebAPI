using AutoMapper;
using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.DTO;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Controllers.StatisticManagement
{
    [Authorize(Roles = "admin")]
    [Route("statistic-management/recipes")]
    public class RecipeStatisticsManagementController : ApiControllerBase
    {
        private readonly IRecipeStatisticsService _recipeStatisticsService;

        public RecipeStatisticsManagementController(IMapper mapper, IRecipeStatisticsService recipeStatisticsService) : base(mapper)
            => _recipeStatisticsService = recipeStatisticsService;

        /// <summary>
        /// Returns the number of recipes created in the given date range.
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns the number of recipes as 'long' (Int64) value </response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        [HttpGet("sum")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ReadSumAsync([FromQuery] TimePeriod command)
        {
            var count = await _recipeStatisticsService.GetRecipesAmountFromPeriodAsync(command);
            return Ok(new { Count = count });
        }

        /// <summary>
        /// Returns the number of recipes per day, created in the given date range.
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns the list of number of recipes as 'long' (Int64) value </response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        [HttpGet("sumPerDay")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ReadSumPerDayAsync([FromQuery] TimePeriod command)
        {
            var sumPerDay = await _recipeStatisticsService.GetRecipesSumPerDayAsync(command);

            return Ok(sumPerDay);
        }

        /// <summary>
        /// Returns the average of recipes per day in the given date range.
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns sum of recipes created in given date range, divided by numer of days in this period </response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        [HttpGet("averagePerDay")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ReadAveragePerDayAsync([FromQuery] TimePeriod command)
        {
            var average = await _recipeStatisticsService.GetAveragePerDayAsync(command);

            return Ok(new { Average = average });
        }

        /// <summary>
        /// Returns the N recipes with best rating, created in a given time period.
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns the list of recipes with best rating, created in a given time period. </response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        [HttpGet("top/creators")]
        [ProducesResponseType(typeof(IEnumerable<RecipeDto>), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ReadTopCreatorsAsync([FromQuery] TopFromTimePeriod command)
        {
            var users = await _recipeStatisticsService.GetTopRatedRecipesAsync(command);
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return Ok(usersDto);
        }
    }
}