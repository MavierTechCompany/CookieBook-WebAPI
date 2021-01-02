using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services
{
    public class UserStatisticsService : IUserStatisticsService
    {
        private readonly CookieContext _context;

        public UserStatisticsService(CookieContext context)
        {
            _context = context;
        }

        public async Task<long> GetUsersAmountFromPeriodAsync(TimePeriod command)
        {
            var startDate = DateTime.SpecifyKind(command.StartDate, DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(command.EndDate, DateTimeKind.Utc);

            return await _context.Users.Where(x => x.CreatedAt.Date >= startDate && x.CreatedAt.Date <= endDate).LongCountAsync();
        }

        //TODO Change this to list of custom objects that have date and userCount. Then it will be serialized as a JSON array
        public async Task<Dictionary<DateTime, long>> GetUsersSumPerDayAsync(TimePeriod command)
        {
            var startDate = DateTime.SpecifyKind(command.StartDate, DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(command.EndDate, DateTimeKind.Utc);

            return await _context.Users
                .Where(x => x.CreatedAt.Date >= startDate && x.CreatedAt.Date <= endDate)
                .GroupBy(x => x.CreatedAt.Date)
                .ToDictionaryAsync(y => y.Key.Date, y => y.LongCount());
        }
    }
}