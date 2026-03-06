using System.Collections;

namespace Secyud.Secits.Blazor.Plugins;

public class SPluginsContainer<TPlugin> : IReadOnlyList<TPlugin> where TPlugin : class, ISPlugin
{
    private readonly List<TPlugin> _settings = [];

    public void TryApply(ISPlugin isPlugin)
    {
        if (isPlugin is not TPlugin t) return;
        // sequence reset
        _settings.Remove(t);
        _settings.Add(t);
    }

    public void TryForgo(ISPlugin isPlugin)
    {
        if (isPlugin is not TPlugin t) return;
        _settings.Remove(t);
    }

    public IEnumerator<TPlugin> GetEnumerator()
    {
        return _settings.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_settings).GetEnumerator();
    }

    public int Count => _settings.Count;

    public TPlugin this[int index] => _settings[index];

    public async Task InvokeAsync(Func<TPlugin, Task> function)
    {
        foreach (var setting in _settings)
        {
            await function(setting);
        }
    }

    public void Invoke(Action<TPlugin> function)
    {
        foreach (var setting in _settings)
        {
            function(setting);
        }
    }
}