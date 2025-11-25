using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class ParameterContainer(ParameterView parameterView)
{
    private readonly List<Lazy<Task>> _parameterTasks = [];
    public ParameterView ParameterView { get; } = parameterView;

    public void UseParameter<TParameter>(TParameter previous, string name, Action<TParameter> action)
    {
        if (TryGetParameterChanged(previous, name, out var value))
            _parameterTasks.Add(new Lazy<Task>(() =>
            {
                action(value);
                return Task.CompletedTask;
            }));
    }

    public void UseParameter<TParameter>(TParameter previous, string name, Func<TParameter, Task> action)
    {
        if (TryGetParameterChanged(previous, name, out var value))
            _parameterTasks.Add(new Lazy<Task>(() => action(value)));
    }

    private bool TryGetParameterChanged<TParameter>(TParameter previous, string name, out TParameter value)
    {
        if (ParameterView.TryGetValue<TParameter>(name, out var v))
        {
            value = v!;
            return !Equals(previous, value);
        }

        value = default!;
        return false;
    }

    public async Task RunAndCleanTasksAsync()
    {
        await Task.WhenAll(_parameterTasks.Select(x => x.Value));
        _parameterTasks.Clear();
    }
}