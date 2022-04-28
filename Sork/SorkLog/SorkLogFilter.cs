using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Sork.SorkLog.Core;

namespace Sork.SorkLog
{
    /// <summary>
    /// Add logging on <c>Controller</c>-level or <c>Method</c>-level with <c>[ServiceFilter(typeof(SorkLog))]</c>
    /// Requires injection in startup <c>services.AddScoped<SorkLog>()</c>
    /// </summary>
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

            context.HttpContext.AddTraceHeader();
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            base.OnResultExecuted(context);

            int code = context.HttpContext.Response.StatusCode;

            _logger.GetLogCommand(code).Execute(context.HttpContext);
        }

    }
}