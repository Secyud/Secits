namespace Secyud.Secits.Blazor.Settings;

public class SSetting<TSetting> where TSetting : class
{
    private TSetting? _setting;

    public void Apply(TSetting setting)
    {
        _setting = setting;
    }

    public void Forgo(TSetting setting)
    {
        if (_setting == setting)
            _setting = null;
    }

    public TSetting? Get() => _setting;

    public async Task InvokeAsync(Func<TSetting, Task> function, Func<Task>? defaultFunction = null)
    {
        if (_setting is not null)
            await function(_setting);
        else if (defaultFunction is not null)
            await defaultFunction();
    }

    public void Invoke(Action<TSetting> function, Action? defaultFunction = null)
    {
        if (_setting is not null)
            function(_setting);
        else
            defaultFunction?.Invoke();
    }
}