using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public class SJsWindow(IJSRuntime js) : IJsWindow
{
    public ValueTask Open(string? uri = null, string? target = null, string? windowFeatures = null)
    {
        return js.InvokeVoidAsync("open", uri, target, windowFeatures);
    }

    public ValueTask OpenOnBlank(string? uri)
    {
        return Open(uri, "_blank");
    }

    public ValueTask Alert(string? message = null)
    {
        return js.InvokeVoidAsync("alert", message);
    }

    public ValueTask AToB(string? str = null)
    {
        return js.InvokeVoidAsync("atob", str);
    }

    public ValueTask BToA(string? str = null)
    {
        return js.InvokeVoidAsync("btoa", str);
    }

    public ValueTask Close()
    {
        return js.InvokeVoidAsync("close");
    }

    public ValueTask Confirm(string? message = null)
    {
        return js.InvokeVoidAsync("confirm", message);
    }

    public ValueTask Focus()
    {
        return js.InvokeVoidAsync("focus");
    }
}