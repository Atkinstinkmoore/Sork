using Microsoft.Extensions.Logging;
using Sork.SorkLog.Interfaces;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Sork.Test")]
namespace Sork.SorkLog.Implementations
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
