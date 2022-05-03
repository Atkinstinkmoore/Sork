using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Interfaces;
using System;
using System.Diagnostics;

namespace Sork.SorkLog.Implementations
{
    internal class LogWarningCommand : ILogCommand
    {
        private readonly ILogger _logger;
        public LogWarningCommand(ILogger logger)
        {
            _logger = logger;
        }
        public void Execute(HttpContext context)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning("traceId: {TraceId}, path: {Path}, method: {Method}, dateTimeUTC: {DateTimeUTC}, statusCode: {StatusCode}",
                    Activity.Current.TraceId,
                    context.Request.Path,
                    context.Request.Method,
                    DateTime.UtcNow,
                    context.Response.StatusCode);
        }

    }
}
