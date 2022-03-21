using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Implementations;
using Sork.SorkLog.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Sork.Test")]
namespace Sork.SorkLog
{
    public class SorkLogFilter : ActionFilterAttribute
    {
        private readonly ILogger<SorkLogFilter> _logger;

        public SorkLogFilter(ILogger<SorkLogFilter> logger)
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

            GetLogCommand(code).Execute(context);
        }

        internal ILogCommand GetLogCommand(int statusCode)
        {
            if (statusCode < 400)
            {
                return new LogInformationCommand(_logger);
            }
            else if (statusCode < 500)
            {
                return  new LogWarningCommand(_logger);
            }
                
            return new LogErrorCommand(_logger);
            
        }

    }
}