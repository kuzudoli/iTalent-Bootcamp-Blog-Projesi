using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace iTalentBootcamp_Blog.Service.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                var exMessage = string.Concat(ex.Message, " - Date -> ", DateTime.Now);
                await WriteFileLog(exMessage);
                _logger.LogError(exMessage);

                await ManipulateResponse(context, exMessage);
            }

        }

        private async Task WriteFileLog(string exMessage)
        {
            var path = ".\\ExceptionLog.txt";
            var fileOptions = new FileStreamOptions
            {
                Access = FileAccess.Write,
                Mode = FileMode.Append
            };

            using (var streamWriter = new StreamWriter(path, Encoding.UTF8, fileOptions))
            {
                await streamWriter.WriteLineAsync(exMessage);
            }
        }

        private async Task ManipulateResponse(HttpContext context, string exMessage)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var resp = JsonConvert.SerializeObject(new { Error = exMessage},Formatting.None);

            await context.Response.WriteAsync(resp);
        }
    }
}
