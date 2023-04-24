using CleanAPI.Application.Exceptions;
using CleanAPI.Presentation.Errors;
using Newtonsoft.Json;
using System.Net;

namespace CleanAPI.Presentation.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var result = string.Empty;

                switch (ex)
                {
                    case NotFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case BadRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case InternalServerException:
                        statusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                    default:
                        break;
                }

                if (string.IsNullOrEmpty(result) && _env.IsDevelopment())
                    result = JsonConvert.SerializeObject(
                        new CodeErrorException(statusCode, ex.Message, ex.StackTrace));
                else
                    result = JsonConvert.SerializeObject(
                        new CodeErrorException(statusCode, ex.Message));

                context.Response.StatusCode = statusCode;

                await context.Response.WriteAsync(result);
            }
        }
    }
}
