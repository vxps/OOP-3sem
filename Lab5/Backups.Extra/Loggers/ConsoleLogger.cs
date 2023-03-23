using Backups.Extra.Interfaces;
using Backups.Extra.Tools;

namespace Backups.Extra.Loggers;

public class ConsoleLogger : ILogger
{
    public void Log(string message, bool timePrefix)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw LoggerException.InvalidMessageException();
        ArgumentNullException.ThrowIfNull(timePrefix);

        if (timePrefix)
            Console.WriteLine(message + " : " + DateTime.Now + '\n');
        else
            Console.WriteLine(message + '\n');
    }
}