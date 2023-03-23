using Backups.Extra.Interfaces;
using Backups.Extra.Tools;

namespace Backups.Extra.Loggers;

public class FileLogger : ILogger
{
    public FileLogger(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw LoggerException.InvalidPathToFileException();

        FilePath = path;
    }

    public string FilePath { get; }
    public void Log(string message, bool timePrefix)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw LoggerException.InvalidMessageException();
        ArgumentNullException.ThrowIfNull(timePrefix);

        if (timePrefix)
            File.AppendAllText(FilePath, message + ": " + DateTime.Now + '\n');
        else
            File.AppendAllText(FilePath, message + '\n');
    }
}