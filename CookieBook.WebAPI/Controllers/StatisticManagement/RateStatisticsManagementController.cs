using AutoMapper;
using CookieBook.Infrastructure.Commands.Statistics;
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
    [Route("statistic-management/rates")]
    public class RateStatisticsManagementController : ApiControllerBase
    {
        private readonly IRateStatisticsService _rateStatisticsService;

        public RateStatisticsManagementController(IMapper mapper, IRateStatisticsService rateStatisticsService) : base(mapper)
            => _rateStatisticsService = rateStatisticsService;

        /// <summary>
        /// Returns the number of users created in the given date range.
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns the number of rates as 'long' (Int64) value </response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        [HttpGet("sum")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ReadSumAsync([FromQuery] TimePeriod command)
        {
            var count = await _rateStatisticsService.GetRatesAmountFromPeriodAsync(command);
            return Ok(new { Count = count });
        }

        /// <summary>
        /// Returns the number of rates per day, created in the given date range.
        /// </summary>
        /// <param name="command"></param>
        /// <response code="200">Returns the list of number of rates as 'long' (Int64) value </response>
        /// <response code="401">Returned when caller/sender doesn't have permission to do this action</response>
        [HttpGet("sumPerDay")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> ReadSumPerDayAsync([FromQuery] TimePeriod command)
        {
            var sumPerDay = await _rateStatisticsService.GetRatesSumPerDayAsync(command);

            return Ok(sumPerDay);
        }
    }
}