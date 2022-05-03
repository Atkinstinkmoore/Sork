using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Interfaces;
using System;
using System.Diagnostics;

namespace Sork.SorkLog.Implementations
{
    internal class LogErrorCommand : ILogCommand
    {
        private readonly ILogger _logger;
        public LogErrorCommand(ILogger logger)
        {
            _logger = logger;
        }
        public void Execute(HttpContext context)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError("traceId: {TraceId}, path: {Path}, dateTimeUTC: {DateTimeUTC}, statusCode: {StatusCode}",
                    Activity.Current.TraceId,
                    context.Request.Path,
                    DateTime.UtcNow,
                    context.Response.StatusCode);
        }

    }
}
