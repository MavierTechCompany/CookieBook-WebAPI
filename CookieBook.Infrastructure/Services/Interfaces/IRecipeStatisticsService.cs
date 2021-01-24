using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IRecipeStatisticsService
    {
        Task<long> GetRecipesAmountFromPeriodAsync(TimePeriod command);

        Task<List<ValueInTime<long>>> GeRecipesSumPerDayAsync(TimePeriod command);

        Task<double> GetAveragePerDayAsync(TimePeriod command);

        Task<IEnumerable<Recipe>> GetTopRatedRecipesAsync(TopFromTimePeriod command);
    }
}