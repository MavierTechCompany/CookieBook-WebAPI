using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services
{
    public class UserStatisticsService : IUserStatisticsService
    {
        public Task<ulong> GetUsersAmountFromPeriod(TimePeriod command) => throw new NotImplementedException();
    }
}