using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IRateStatisticsService
    {
        Task<long> GetRatesAmountFromPeriodAsync(TimePeriod command);

        Task<List<ValueInTime<long>>> GetRatesSumPerDayAsync(TimePeriod command);
    }
}