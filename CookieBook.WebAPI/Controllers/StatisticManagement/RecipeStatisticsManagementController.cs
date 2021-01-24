using AutoMapper;
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
        public RecipeStatisticsManagementController(IMapper mapper) : base(mapper)
        {
        }
    }
}