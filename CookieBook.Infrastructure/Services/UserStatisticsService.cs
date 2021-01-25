using CookieBook.Domain.Models;
using CookieBook.Infrastructure.Commands.Statistics;
using CookieBook.Infrastructure.Data;
using CookieBook.Infrastructure.Extensions;
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

        public UserStatisticsService(CookieContext context) => _context = context;

        public async Task<double> GetAveragePerDayAsync(TimePeriod command)
        {
            var startDate = DateTime.SpecifyKind(command.StartDate, DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(command.EndDate, DateTimeKind.Utc);
            var days = (endDate - startDate).TotalDays;

            var count = await _context.Users
                .Where(x => x.CreatedAt.Date >= startDate && x.CreatedAt.Date <= endDate).LongCountAsync();

            return count / days;
        }

        public async Task<IEnumerable<User>> GetTopCreatorsAsync(TopFromTimePeriod command)
        {
            var startDate = DateTime.SpecifyKind(command.StartDate, DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(command.EndDate, DateTimeKind.Utc);

            var usersWithScore = await _context.Recipes
                .Where(x => x.CreatedAt.Date >= startDate && x.CreatedAt.Date <= endDate)
                .GroupBy(x => x.User)
                .ToDictionaryAsync(x => x.Key, x => x.Count());

            return usersWithScore.OrderByDescending(x => x.Value).Take(command.Amount).Select(x => x.Key);
        }

        public async Task<long> GetUsersAmountFromPeriodAsync(TimePeriod command)
        {
            var startDate = DateTime.SpecifyKind(command.StartDate, DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(command.EndDate, DateTimeKind.Utc);

            return await _context.Users.Where(x => x.CreatedAt.Date >= startDate && x.CreatedAt.Date <= endDate).LongCountAsync();
        }

        public async Task<List<ValueInTime<long>>> GetUsersSumPerDayAsync(TimePeriod command)
        {
            var startDate = DateTime.SpecifyKind(command.StartDate, DateTimeKind.Utc);
            var endDate = DateTime.SpecifyKind(command.EndDate, DateTimeKind.Utc);

            return await _context.Users
                .Where(x => x.CreatedAt.Date >= startDate && x.CreatedAt.Date <= endDate)
                .GroupBy(x => x.CreatedAt.Date)
                .Select(x => new ValueInTime<long>() { Value = x.LongCount(), Date = x.Key })
                .ToListAsync();
        }
    }
}