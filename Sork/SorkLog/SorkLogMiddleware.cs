using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Implementations;
using System.Threading.Tasks;

namespace Sork.SorkLog
{
    public class SorkLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<SorkLogMiddleware> _logger;

        public SorkLogMiddleware(RequestDelegate next, ILogger<SorkLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            int code = context.Response.StatusCode;
            _logger.GetLogCommand(code).Execute(context);
        }
    }

    public static class SorkLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseSorkLog(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SorkLogMiddleware>();
        }
    }
}
