using Microsoft.AspNetCore.Http;

namespace Sork.SorkLog.Interfaces
{
    internal interface ILogCommand
    {
        public void Execute(HttpContext context);
    }
}
