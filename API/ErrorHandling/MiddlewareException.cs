using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.ErrorHandling;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    public class MiddlewareException
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<MiddlewareException> _logger;
        private readonly IHostEnvironment _env;
        public MiddlewareException(RequestDelegate next, ILogger<MiddlewareException> logger, IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
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
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ServerException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ServerException((int)HttpStatusCode.InternalServerError);
              
                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response , options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}