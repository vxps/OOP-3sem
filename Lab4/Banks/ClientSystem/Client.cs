using Banks.Observer;
using Banks.Tools;

namespace Banks.ClientSystem;

public class Client : IObserver
{
    private List<string> _notifications;
    public Client(string name, string surname, string? address = null, string? passport = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw ClientException.InvalidClientName();
        if (string.IsNullOrWhiteSpace(surname))
            throw ClientException.InvalidClientSurname();

        Name = name;
        Surname = surname;
        Address = address;
        Passport = passport;
        Id = Guid.NewGuid();
        _notifications = new List<string>();
    }

    public string Name { get; }
    public string Surname { get; }
    public Guid Id { get; }
    public string? Address { get; }
    public string? Passport { get; }
    public IReadOnlyList<string> Notifications => _notifications;

    public bool CheckVerification() => Passport != null && Address != null;

    public void Update(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            throw ClientException.InvalidNotificationMessage();

        _notifications.Add(message);
    }
}