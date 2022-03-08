using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace Sork
{
    public class SorkLog : ActionFilterAttribute
    {
        private readonly ILogger<SorkLog> _logger;

        public SorkLog(ILogger<SorkLog> logger)
        {
            _logger = logger;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);

            int code = context.HttpContext.Response.StatusCode;

            LoggingDelegate logDelegate;

            if (code < 400)
            {
                logDelegate = LogInformationDelegate;
            }
            else if (code < 500)
            {
                logDelegate = LogWarningDelegate;
            }
            else
            {
                logDelegate = LogErrorDelegate;
            }
            logDelegate.Invoke(context);
        }

        private delegate void LoggingDelegate(ResultExecutedContext context);

        private void LogInformationDelegate(ResultExecutedContext context)
        {
            if (_logger.IsEnabled(LogLevel.Information))
                _logger.LogInformation(FormatMessage(context));

        }

        private void LogWarningDelegate(ResultExecutedContext context)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
                _logger.LogWarning(FormatMessage(context));

        }

        private void LogErrorDelegate(ResultExecutedContext context)
        {
            if (_logger.IsEnabled(LogLevel.Error))
                _logger.LogError(FormatMessage(context));

        }

        private string FormatMessage(ResultExecutedContext context)
        {
            return $"{{'route': {context.RouteData}, 'action': {context.ActionDescriptor.DisplayName}, 'responseCode': {context.HttpContext.Response.StatusCode}}} 'timeOfCallUTC': {DateTime.Now.ToUniversalTime()}";
        }
    }
}