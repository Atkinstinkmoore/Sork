using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Interfaces;
using System;

namespace Sork.SorkLog.Implementations
{
    internal class LogInformationCommand : ILogCommand
    {
        private readonly ILogger _logger;
        public LogInformationCommand(ILogger logger)
        {
            _logger = logger;
        }
        public void Execute(HttpContext context)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("trace: {Trace}, path: {Route}, time: {Time}, statusCode: {StatusCode}",
                    context.TraceIdentifier,
                    context.Request.Path,
                    DateTime.Now.ToUniversalTime(),
                    context.Response.StatusCode);
        }

    }
}
