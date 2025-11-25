using System.Collections;

namespace Secyud.Secits.Blazor.Settings;

public class SSettings<TSetting> : IReadOnlyList<TSetting> where TSetting : class
{
    private readonly List<TSetting> _settings = [];

    public void Apply(TSetting setting)
    {
        _settings.Remove(setting);
        _settings.Add(setting);
    }

    public void Forgo(TSetting setting)
    {
        _settings.Remove(setting);
    }

    public IEnumerator<TSetting> GetEnumerator()
    {
        return _settings.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_settings).GetEnumerator();
    }

    public int Count => _settings.Count;

    public TSetting this[int index] => _settings[index];

    public async Task InvokeAsync(Func<TSetting, Task> function)
    {
        foreach (var setting in _settings)
        {
            await function(setting);
        }
    }

    public void Invoke(Action<TSetting> function)
    {
        foreach (var setting in _settings)
        {
            function(setting);
        }
    }
}