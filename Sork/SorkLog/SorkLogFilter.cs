using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Implementations;

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

            _logger.GetLogCommand(code).Execute(context.HttpContext);
        }

    }
}