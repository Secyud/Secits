using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JsInterop;

public sealed class JsEvent<TElement, TArgs> : IAsyncDisposable
{
    private readonly string _eventName;
    private readonly TElement _element;
    private readonly IJSRuntime _js;
    private readonly DotNetObjectReference<JsEvent<TElement, TArgs>> _ref;

    public JsEvent(TElement element, string eventName, IJSRuntime js)
    {
        _eventName = eventName;
        _element = element;
        _js = js;
        _ref = DotNetObjectReference.Create(this);
    }

    public event Func<TArgs, Task>? Event;

    [JSInvokable("invoke")]
    public Task Invoke(TArgs args)
    {
        return Event?.Invoke(args) ?? Task.CompletedTask;
    }

    public ValueTask CreateEventAsync(bool preventDefault = false, bool stopPropagation = false)
    {
        return _js.InvokeVoidAsync(SJsModules.Event.Create,
            _element, _eventName, _ref, preventDefault, stopPropagation);
    }

    public ValueTask DeleteEventAsync()
    {
        return _js.InvokeVoidAsync(SJsModules.Event.Delete,
            _element, _eventName, _ref);
    }

    public async ValueTask DisposeAsync()
    {
        _ref.Dispose();
        await DeleteEventAsync();
    }
}