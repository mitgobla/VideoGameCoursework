using System.Collections.Generic;

public class ServiceLocator
{

    private ServiceLocator() { }

    private static Dictionary<string, object> _services = new Dictionary<string, object>();

    public static ServiceLocator Instance { get; private set; }

    public static void Initialize()
    {
        Instance = new ServiceLocator();
    }

    public void Register<T>(T service) where T : IGameService
    {
        if (_services.ContainsKey(typeof(T).Name))
        {
            return;
        }
        _services.Add(typeof(T).Name, service);
    }
    public T Get<T>() where T : IGameService
    {
        return (T)_services[typeof(T).Name];
    }

    public void Unregister<T>() where T : IGameService
    {
        _services.Remove(typeof(T).Name);
    }

    public void Clear()
    {
        _services.Clear();
    }
}