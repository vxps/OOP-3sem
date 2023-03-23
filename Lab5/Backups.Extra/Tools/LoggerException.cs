namespace Backups.Extra.Tools;

public class LoggerException : Exception
{
    public LoggerException(string message)
        : base(message) { }

    public static LoggerException InvalidPathToFileException()
    {
        throw new LoggerException("path to file is null or white space");
    }

    public static LoggerException InvalidMessageException()
    {
        throw new LoggerException("message is null or white space");
    }
}