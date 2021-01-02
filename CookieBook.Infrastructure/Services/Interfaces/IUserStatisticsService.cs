﻿using CookieBook.Infrastructure.Commands.Statistics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IUserStatisticsService
    {
        Task<ulong> GetUsersAmountFromPeriod(TimePeriod command);
    }
}