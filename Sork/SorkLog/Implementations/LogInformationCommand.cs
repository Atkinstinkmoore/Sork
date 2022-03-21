using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Interfaces;
using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Sork.Test")]
namespace Sork.SorkLog.Implementations
{
    internal class LogInformationCommand : ILogCommand
    {
        private readonly ILogger _logger;
        public LogInformationCommand(ILogger logger)
        {
            _logger = logger;
        }
        public void Execute(ResultExecutedContext context)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation("route: {Route}, time: {Time}, statusCode: {StatusCode}",
                    context.RouteData,
                    DateTime.Now.ToUniversalTime(),
                    context.HttpContext.Response.StatusCode);
        }

    }
}
