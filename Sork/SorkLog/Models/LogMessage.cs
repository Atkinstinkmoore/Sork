using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;

namespace Sork.SorkLog.Models
{
    internal record LogMessage(ActivityTraceId TraceId, PathString Path, DateTime DateTimeUTC, int StatusCode)
    {
        public override string ToString()
        {
            return $"'traceId': '{TraceId}', 'path': '{Path}', 'dateTimeUTC': '{DateTimeUTC}', 'statusCode': {StatusCode}";
        }
    }
}
