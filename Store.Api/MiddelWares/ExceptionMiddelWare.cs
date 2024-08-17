using Store.Service.HandelResponces;
using System.Net;
using System.Text.Json;

namespace Store.Api.MiddelWares
{
    public class ExceptionMiddelWare
    {
        private readonly RequestDelegate next;
        private readonly IHostEnvironment environment;
        private readonly ILogger<ExceptionMiddelWare> logger;

        public ExceptionMiddelWare(RequestDelegate next,
            IHostEnvironment environment,
            ILogger<ExceptionMiddelWare> logger)
            
        {
            this.next = next;
            this.environment = environment;
            this.logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode=(int) HttpStatusCode.InternalServerError;
                var response = environment.IsDevelopment()
                    ? new CustomException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new CustomException((int)HttpStatusCode.InternalServerError);
                var options= new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
