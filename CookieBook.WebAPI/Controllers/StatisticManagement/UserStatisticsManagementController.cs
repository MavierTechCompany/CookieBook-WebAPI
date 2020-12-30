using AutoMapper;
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
        public UserStatisticsManagementController(IMapper mapper) : base(mapper)
        {
        }

        //TODO Need to wrap parameters into a class
        [HttpGet("sum")]
        public async Task<IActionResult> ReadSumAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate) => throw new NotImplementedException();

        [HttpGet("sumPerDay")]
        public async Task<IActionResult> ReadSumPerDayAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate) => throw new NotImplementedException();

        [HttpGet("average")]
        public async Task<IActionResult> ReadAverageAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate) => throw new NotImplementedException();

        [HttpGet("topCreators")]
        public async Task<IActionResult> ReadTopCreatorsAsync([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery] uint amount) => throw new NotImplementedException();
    }
}