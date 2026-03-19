using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JsInterop;

public class JsInvoker
{
    private readonly Func<Task> _event;

    public JsInvoker(Func<Task> @event)
    {
        _event = @event;
        Ref = DotNetObjectReference.Create(this);
    }

    [JSInvokable("invoke")]
    public Task Invoke()
    {
        return _event.Invoke();
    }
    
    public DotNetObjectReference<JsInvoker> Ref { get; }
}