using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Sork.SorkLog.Core
{
    internal static class HttpHeaderExtension
    {
        const string _headerName = "X-Correlation-ID";
        internal static void AddTraceHeader(this HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(_headerName, Activity.Current.TraceId.ToString());
                return Task.CompletedTask;
            });
            
        }
    }
}
