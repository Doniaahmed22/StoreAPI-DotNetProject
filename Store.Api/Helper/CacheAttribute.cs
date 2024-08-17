using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Store.Service.Services.CashService;
using System.Text;

namespace Store.Api.Helper
{
    public class CacheAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int timeToLiveInSeconed;

        public CacheAttribute(int timeToLiveInSeconed)
        {
            this.timeToLiveInSeconed = timeToLiveInSeconed;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var _cahceServise = context.HttpContext.RequestServices.GetRequiredService<ICachService>();
           var cachKey= GenerateCachKeyFromRequest(context.HttpContext.Request);
            var cachResponse= await _cahceServise.GetCacheResponseAsync(cachKey);
            if (!string.IsNullOrEmpty(cachResponse))
            {
                var contentRequest = new ContentResult()
                {
                    Content = cachResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };
                context.Result = contentRequest;
                return;
            }
            var executeContext = await next();
            if (executeContext.Result is OkObjectResult response)
            {
                await _cahceServise.SetCacheResponseAsync(cachKey, response.Value, TimeSpan.FromSeconds(timeToLiveInSeconed));
            }
        }

        private string GenerateCachKeyFromRequest(HttpRequest request)
        {
            StringBuilder caheKey = new StringBuilder();
            caheKey.Append($"{request.Path}");
            foreach ( var (key,value) in request.Query.OrderBy(x=> x.Key) ) 
            {
            caheKey.Append($"|{key}-{value}");
            }
            return caheKey.ToString();  
        }
    }
}
