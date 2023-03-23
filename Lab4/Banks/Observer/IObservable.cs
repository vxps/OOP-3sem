using Banks.ClientSystem;

namespace Banks.Observer;

public interface IObservable
{
    void AddObserver(IObserver observer);
    void RemoveObserver(IObserver observer);
    void NotifyObservers(string setting, decimal newLimit);
}