using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTalentBootcamp_Blog.Service.Middlewares
{
    public class HelloMiddleware
    {
        RequestDelegate _next;//Kısa devre olmaması için sonraki middleware'in delegate'i alınır
        public HelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Hoşgeldin!");
            await _next.Invoke(httpContext);//_next'i invoke etmezsek Run metodundaki gibi kısa devre olacaktır.
            Console.WriteLine("Hoşgittin!");
        }
    }
}
