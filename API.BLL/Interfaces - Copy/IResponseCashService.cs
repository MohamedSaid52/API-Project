using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.BLL.Interfaces___Copy
{
    public interface IResponseCashService
    {
        Task CashResponseAsync(string CashKey, object Response, TimeSpan timetolive);
        Task<string> GetCashResponse(string CashKey);
    }
}
