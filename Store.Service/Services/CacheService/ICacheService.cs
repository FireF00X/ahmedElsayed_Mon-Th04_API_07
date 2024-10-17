using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.Services.CacheService
{
    public interface ICacheService
    {
        Task SetCacheServiceAsync(string key, object response, TimeSpan timeToLive);
        Task<string> GetCacheServiceAsync(string key);
    }
}
