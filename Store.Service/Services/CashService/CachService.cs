using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.Service.Services.CashService
{
    public class CachService : ICachService
    {
        private readonly IDatabase _database;
        public CachService(IConnectionMultiplexer redis) 
        {
            _database=redis.GetDatabase();
        }    
        public async Task<string> GetCacheResponseAsync(string key)
        {
          var cachResponce= await _database.StringGetAsync(key);
            if (cachResponce.IsNullOrEmpty)
            {
                return null;
            }
            return cachResponce.ToString(); 
        }

        public async Task SetCacheResponseAsync(string key, object responce, TimeSpan timeToLive)
        {
            if (responce is null)
            {
                return;
            }
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var serializedResponse=JsonSerializer.Serialize(responce, options);
            await _database.StringSetAsync(key, serializedResponse, timeToLive);
        }
    }
}
