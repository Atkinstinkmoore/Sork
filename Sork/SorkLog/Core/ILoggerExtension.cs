using Microsoft.Extensions.Logging;
using Sork.SorkLog.Interfaces;
using Sork.SorkLog.Implementations;

namespace Sork.SorkLog.Core
{
    internal static class ILoggerExtension
    {
        internal static ILogCommand GetLogCommand<T>(this ILogger<T> logger, int statusCode)
        {
            if (statusCode < 400)
            {
                return new LogInformationCommand(logger);
            }
            else if (statusCode < 500)
            {
                return new LogWarningCommand(logger);
            }

            return new LogErrorCommand(logger);

        }
    }
}
