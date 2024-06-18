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
            // Create a composite key using username and platform
            var cacheKey = $"{username}_{platform}";

            if (!_cache.TryGetValue(cacheKey, out string token))
            {
                // Token not in cache, fetch from database
                token = await _db.GetTokenByUsername(username, platform);

                if (token != null)
                {
                    // Set cache options
                    var cacheEntryOptions = new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    };

                    // Save data in cache with the composite key
                    _cache.Set(cacheKey, token, cacheEntryOptions);
                }
            }

            return token;
        }
    }
}
