using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public abstract class JsElementBase(IJSRuntime js)
{
    protected async ValueTask InvokeVoidAsync(ElementReference element, string identifier, params object?[]? args)
    {
        try
        {
            await js.InvokeVoidAsync("invokeElementMethodVoid", element, identifier, args);
        }
        catch (JSDisconnectedException)
        {
            // ignored
        }
    }

    protected async ValueTask<T> InvokeAsync<T>(ElementReference element, string identifier, params object?[]? args)
        where T : new()
    {
        try
        {
            return await js.InvokeAsync<T>("invokeElementMethod", element, identifier, args);
        }
        catch (JSDisconnectedException)
        {
            return new T();
        }
    }
}