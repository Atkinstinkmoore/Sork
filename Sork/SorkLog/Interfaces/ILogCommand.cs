using Microsoft.AspNetCore.Mvc.Filters;

namespace Sork.SorkLog.Interfaces
{
    internal interface ILogCommand
    {
        public void Execute(ResultExecutedContext context);
    }
}
