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
                _logger.LogError("trace: {Trace}, path: {Route}, time: {Time}, statusCode: {StatusCode}",
                    Activity.Current.TraceId,
                    context.Request.Path,
                    DateTime.Now.ToUniversalTime(),
                    context.Response.StatusCode);
        }

    }
}
