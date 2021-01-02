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

        //TODO Need to wrap parameters into a class
        [HttpGet("sum")]
        public async Task<IActionResult> ReadSumAsync([FromQuery] TimePeriod command) => Ok();

        [HttpGet("sumPerDay")]
        public async Task<IActionResult> ReadSumPerDayAsync([FromQuery] TimePeriod command) => throw new NotImplementedException();

        [HttpGet("average")]
        public async Task<IActionResult> ReadAverageAsync([FromQuery] TimePeriod command) => throw new NotImplementedException();

        [HttpGet("topCreators")]
        public async Task<IActionResult> ReadTopCreatorsAsync([FromQuery] TopFromTimePeriod command) => throw new NotImplementedException();
    }
}