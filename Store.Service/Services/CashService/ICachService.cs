using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.CashService
{
    public interface ICachService
    {
        Task SetCacheResponseAsync(string key, object responce, TimeSpan timeToLive);
        Task<string> GetCacheResponseAsync(string key);




    }
}
