namespace Secyud.Secits.Blazor.Plugins;

public class SPluginContainer<TPlugin> where TPlugin : class, ISPlugin
{
    private TPlugin? _setting;

    public void TryApply(ISPlugin isPlugin)
    {
        if (isPlugin is not TPlugin t) return;
        _setting = t;
    }

    public void TryForgo(ISPlugin isPlugin)
    {
        if (isPlugin is not TPlugin t) return;
        if (_setting == t)
            _setting = null;
    }

    public TPlugin? Get() => _setting;

    public async Task InvokeAsync(Func<TPlugin, Task> function, Func<Task>? defaultFunction = null)
    {
        if (_setting is not null)
            await function(_setting);
        else if (defaultFunction is not null)
            await defaultFunction();
    }

    public void Invoke(Action<TPlugin> function, Action? defaultFunction = null)
    {
        if (_setting is not null)
            function(_setting);
        else
            defaultFunction?.Invoke();
    }
}