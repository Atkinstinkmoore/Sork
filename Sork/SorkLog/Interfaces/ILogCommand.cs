using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Sork.SorkLog.Interfaces
{
    internal interface ILogCommand
    {
        public void Execute(HttpContext context);
    }
}
