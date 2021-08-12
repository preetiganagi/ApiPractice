using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ApiPractice
{
    public class MiddlewareTest
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public MiddlewareTest(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;

            _logger = logFactory.CreateLogger("MyMiddleware");
        }

        public async Task Invoke(HttpContext httpContext)
        {

            Console.WriteLine("from middleware");
            _logger.LogInformation("MyMiddleware executing..");

            await _next(httpContext); // calling next middleware

        }
    }


    //// Extension method used to add the middleware to the HTTP request pipeline.
    //public static class MyMiddlewareExtensions
    //{
    //    public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
    //    {
    //        return builder.UseMiddleware<MiddlewareTest>();
    //    }
    //}
}
