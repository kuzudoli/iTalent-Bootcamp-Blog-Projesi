using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

                //Console Loglama
                _logger.LogError(exMessage);
            }
            
        }
    }
}
