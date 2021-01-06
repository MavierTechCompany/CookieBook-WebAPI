using AutoMapper;
using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.Services.Interfaces;
using CookieBook.WebAPI.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookieBook.WebAPI.Controllers.StatisticManagement
{
    [Route("statistic-management/users")]
    public class UserStatisticsManagementController : ApiControllerBase
    {
        private readonly IUserStatisticsService _userStatisticsService;

        public UserStatisticsManagementController(IMapper mapper, IUserStatisticsService userStatisticsService) : base(mapper)
        {
            _userStatisticsService = userStatisticsService;
        }

        [HttpGet("sum")]
        public async Task<IActionResult> ReadSumAsync([FromQuery] TimePeriod command)
        {
            var count = await _userStatisticsService.GetUsersAmountFromPeriodAsync(command);

            return Ok(new { Count = count });
        }

        [HttpGet("sumPerDay")]
        public async Task<IActionResult> ReadSumPerDayAsync([FromQuery] TimePeriod command)
        {
            var sumPerDay = await _userStatisticsService.GetUsersSumPerDayAsync(command);

            return Ok(sumPerDay);
        }

        [HttpGet("averagePerDay")]
        public async Task<IActionResult> ReadAveragePerDayAsync([FromQuery] TimePeriod command)
        {
            var average = await _userStatisticsService.GetAveragePerDayAsync(command);

            return Ok(new { Average = average });
        }

        [HttpGet("top")]
        public async Task<IActionResult> ReadTopCreatorsAsync([FromQuery] TopFromTimePeriod command) => throw new NotImplementedException();
    }
}