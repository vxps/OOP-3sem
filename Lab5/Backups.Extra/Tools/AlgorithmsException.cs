namespace Backups.Extra.Tools;

public class AlgorithmsException : Exception
{
    public AlgorithmsException(string message)
        : base(message) { }

    public static AlgorithmsException InvalidCountInAlgorithmsException()
    {
        throw new AlgorithmsException("path to file is null or white space");
    }

    public static AlgorithmsException InvalidChooseException()
    {
        throw new AlgorithmsException("all restore points selected to delete");
    }
}