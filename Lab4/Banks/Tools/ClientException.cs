namespace Banks.Tools;

public class ClientException : Exception
{
    public ClientException(string message)
        : base(message) { }

    public static ClientException InvalidClientName()
    {
        throw new ClientException("client name is null or whitespace");
    }

    public static ClientException InvalidClientNameFindException()
    {
        throw new ClientException("can't find such client");
    }

    public static ClientException InvalidNotificationMessage()
    {
        throw new ClientException("message is null or whitespace");
    }

    public static ClientException InvalidClientSurname()
    {
        throw new ClientException("client surname is null or whitespace");
    }

    public static ClientException InvalidClientAddress()
    {
        throw new ClientException("client address is null or whitespace");
    }

    public static ClientException InvalidClientPassport()
    {
        throw new ClientException("client passport is null or whitespace");
    }
}