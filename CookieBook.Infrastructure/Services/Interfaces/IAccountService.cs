using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookieBook.Infrastructure.Services.Interfaces
{
    public interface IAccountService
    {
        Task<bool> IsActive(int id);
    }
}