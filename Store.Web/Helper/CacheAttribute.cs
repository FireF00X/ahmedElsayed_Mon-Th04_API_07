using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.Services.CacheService;
using System.Text;

namespace Store.Web.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveInSecondes;

        public CacheAttribute(int timeToLiveInSecondes) 
        {
            _timeToLiveInSecondes = timeToLiveInSecondes;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cacheService = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();

            var cachKey = GenerateCahceKeyFromRequset(context.HttpContext.Request);

            var cachedResponse = await _cacheService.GetCacheServiceAsync(cachKey);

            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                
                context.Result = contentResult;

                return;
            }

            var executedContext = await next();

            if (executedContext.Result is OkObjectResult response)
            {
                await _cacheService.SetCacheServiceAsync(cachKey , response.Value, TimeSpan.FromSeconds(_timeToLiveInSecondes));
            }

        }

        private string GenerateCahceKeyFromRequset(HttpRequest requset)
        {
            StringBuilder cacheKey = new StringBuilder();

            cacheKey.Append($"{requset.Path}");

            foreach ( var (key, value) in requset.Query.OrderBy(x => x.Key) )
            {
                cacheKey.Append($"|{key}-{value}");
            }

            return cacheKey.ToString();
        }
    }
}
