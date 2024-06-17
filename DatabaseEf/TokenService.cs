using DatabaseEf.Entities.Enums;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEf
{
    public class TokenService
    {
        private readonly IMemoryCache _cache;
        private readonly DataContext _db; // Your database context

        public TokenService(IMemoryCache cache, DataContext db)
        {
            _cache = cache;
            _db = db;
        }

        public async Task<string> GetTokenAsync(string username, ServiceName platform)
        {
            if (!_cache.TryGetValue(username, out string token))
            {
                // Token not in cache, fetch from database
                token = await _db.GetTokenByUsername(username, platform); 

                // Set cache options
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                };

                // Save data in cache
                _cache.Set(username, token, cacheEntryOptions);
            }

            return token;
        }
    }
}
