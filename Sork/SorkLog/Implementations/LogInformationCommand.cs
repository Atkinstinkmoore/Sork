using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Interfaces;
using Sork.SorkLog.Models;
using System;
using System.Diagnostics;

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
                _logger.LogInformation(new LogMessage(Activity.Current.TraceId, context.Request.Path, DateTime.UtcNow, context.Response.StatusCode).ToString());
        }

    }
}
