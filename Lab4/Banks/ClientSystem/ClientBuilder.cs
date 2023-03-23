using Banks.Tools;

namespace Banks.ClientSystem;

public class ClientBuilder
{
    public const int MinPassport = 1000000000;
    private string _name = string.Empty;
    private string _surname = string.Empty;
    private string? _address = null;
    private string? _passport = null;

    public void AddName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw ClientException.InvalidClientName();

        _name = name;
    }

    public void AddSurname(string surname)
    {
        if (string.IsNullOrWhiteSpace(surname))
            throw ClientException.InvalidClientSurname();

        _surname = surname;
    }

    public void AddAddress(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw ClientException.InvalidClientAddress();

        _address = address;
    }

    public void AddPassport(string passport)
    {
        if (string.IsNullOrWhiteSpace(passport))
            throw ClientException.InvalidClientPassport();
        if (Convert.ToDecimal(passport) < MinPassport)
            throw ClientException.InvalidClientPassport();

        _passport = passport;
    }

    public Client MakeClient() => new Client(_name, _surname, _address, _passport);
}