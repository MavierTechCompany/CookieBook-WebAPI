using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IUserStatisticsService
    {
        Task<long> GetUsersAmountFromPeriodAsync(TimePeriod command);

        Task<List<ValueInTime<long>>> GetUsersSumPerDayAsync(TimePeriod command);
    }
}