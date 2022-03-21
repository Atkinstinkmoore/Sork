using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Interfaces;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Sork.Test")]
namespace Sork.SorkLog.Implementations
{
    internal class LogWarningCommand : ILogCommand
    {
        private readonly ILogger _logger;
        public LogWarningCommand(ILogger logger)
        {
            _logger = logger;
        }
        public void Execute(ResultExecutedContext context)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning("route: {Route}, time: {Time}, statusCode: {StatusCode}",
                    context.RouteData,
                    DateTime.Now.ToUniversalTime(),
                    context.HttpContext.Response.StatusCode);
        }

    }
}
