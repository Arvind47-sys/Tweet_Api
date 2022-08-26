using Microsoft.AspNetCore.Builder;
using Tweet_Api.Middlewares;

namespace Tweet_Api.Extensions
{
    public static class RequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestMiddleware>();
        }
    }
}