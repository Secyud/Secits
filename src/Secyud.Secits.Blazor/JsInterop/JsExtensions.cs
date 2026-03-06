using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.JSInterop;

namespace Secyud.Secits.Blazor.JsInterop;

public static class JsExtensions
{
    extension(ElementReference element)
    {
        public ValueTask<DomRect> GetBoundingClientRect(IJSRuntime js)
        {
            return element.InvokeAsync<DomRect>(js, "getBoundingClientRect");
        }


        public async ValueTask InvokeVoidAsync(IJSRuntime js, string identifier, params object?[]? args)
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

        public async ValueTask<T> InvokeAsync<T>(IJSRuntime js, string identifier, params object?[]? args)
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
}