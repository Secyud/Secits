using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public abstract class JsModule(IJSRuntime js, string moduleUrl) : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask =
        new(() => js.InvokeAsync<IJSObjectReference>("import", moduleUrl).AsTask());

    private readonly CancellationTokenSource _cts = new();
    private bool _isDisposed;

    public async ValueTask InvokeVoidAsync(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;

        if (_cts.IsCancellationRequested)
        {
            return;
        }

        try
        {
            await module.InvokeVoidAsync(identifier, args);
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
    }

    public async ValueTask<T?> InvokeAsync<T>(string identifier, params object?[]? args)
    {
        var module = await _moduleTask.Value;

        if (_cts.Token.IsCancellationRequested)
        {
            return default;
        }

        try
        {
            return await module.InvokeAsync<T>(identifier, args);
        }
        catch (JSDisconnectedException)
        {
            return default;
        }
    }

    protected virtual ValueTask DisposeAsyncCore() => ValueTask.CompletedTask;

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        if (_isDisposed)
        {
            return;
        }

        await _cts.CancelAsync();

        if (_moduleTask is { IsValueCreated: true, Value.IsFaulted: false })
        {
            var module = await _moduleTask.Value;

            try
            {
                await DisposeAsyncCore().ConfigureAwait(false);
                await module.DisposeAsync().ConfigureAwait(false);
            }
            catch (JSDisconnectedException)
            {
                // ignored
            }
        }

        _isDisposed = true;
        GC.SuppressFinalize(this);
    }
}