using System.Timers;
using Microsoft.AspNetCore.Components;
using Timer = System.Timers.Timer;

namespace Secyud.Secits.Blazor.Plugins;

public class SpInputDelay<TValue> : SPluginBase, ISpInputHandler
{
    private const double DefaultIntervalSeconds = 1;

    public override string PluginName => "input-delay";

    [Parameter]
    public TimeSpan Interval
    {
        get;
        set
        {
            if (field == value) return;
            _timer.Dispose();
            field = value;
            _timer = new Timer(value);
            _timer.AutoReset = false;
            _timer.Elapsed += OnElapsed;
        }
    } = TimeSpan.FromSeconds(DefaultIntervalSeconds);

    private Timer _timer = null!;

    private string? _cachedValue;

    public async Task HandleInputAsync(string? str)
    {
        _timer.Stop();
        _cachedValue = str;
        _timer.Start();
        await Task.CompletedTask;
    }

    private void OnElapsed(object? sender, ElapsedEventArgs args)
    {
        TriggerValueChangedEventAsync().ConfigureAwait(false);
    }

    private async Task TriggerValueChangedEventAsync()
    {
        if (Context?.Component is EComponentBase<TValue> c)
        {
            await c.TriggerInputChangedEventAsync(_cachedValue);
        }
    }

    protected override void Dispose(bool isDisposing)
    {
        base.Dispose(isDisposing);
        _timer.Dispose();
    }
}