using iTalentBootcamp_Blog.Service.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace iTalentBootcamp_Blog.Service.Extensions
{
    public static class Extension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleware>();
        }
    }
}
