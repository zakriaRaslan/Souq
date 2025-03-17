using Microsoft.Extensions.Caching.Memory;
using Souq.Api.Hellpers;
using System.Net;
using System.Text.Json;

namespace Souq.Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);
        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _next = next;
            _environment = environment;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplySecurity(context);
                if(IsRequestAllowed(context) == false)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    ApiException exception = new ApiException((int)HttpStatusCode.TooManyRequests, "Too many requeste please try later");
                   
                    await context.Response.WriteAsJsonAsync(exception);
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                ApiException exeption = _environment.IsDevelopment()?
                    new ApiException((int)HttpStatusCode.InternalServerError ,ex.Message ,ex.StackTrace )
                    : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message);
                var jsonException = JsonSerializer.Serialize(exeption); 
                await context.Response.WriteAsync(jsonException);
            }
        }


        private bool IsRequestAllowed(HttpContext context) 
        { 
            IPAddress? userIp = context.Connection.RemoteIpAddress;
            string cachKey = $"Rate:{userIp}";
            DateTime dateNow = DateTime.Now;

            var (timesTamp, count) = _memoryCache.GetOrCreate(cachKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (timesTamp: dateNow, count: 0);
            });

            if (dateNow - timesTamp < _rateLimitWindow)
            {
                if (count >= 8)
                {
                    return false;
                }
                _memoryCache.Set(cachKey, (timesTamp, count += 1), _rateLimitWindow);
            }
            else 
            {
                _memoryCache.Set(cachKey, (timesTamp, 1), _rateLimitWindow);
            }
            return true;
        }


        private void ApplySecurity (HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-XSS-Protection"] = "1;mode=block";
            context.Response.Headers["X-Frame-Options"] = "DENY";
        }
    }   
}
