using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public class SJsDocument(JsEventHandler eventHandler) : IJsDocument
{
    private readonly Lazy<Task<IJSObjectReference>> _documentEventHandler =
        new(() => eventHandler.InvokeAsync<IJSObjectReference>("getDocumentEventHandler").AsTask()!);

    public Task<long> AddEventListenerAsync<TEventArgs>(Func<TEventArgs, Task> func, params string[] types)
    {
        return AddEventListenerAsync(new Invoker<TEventArgs>(func), types);
    }

    public Task<long> AddEventListenerAsync(Func<Task> func, params string[] types)
    {
        return AddEventListenerAsync(new Invoker(func), types);
    }

    private async Task<long> AddEventListenerAsync(object invoker, string[] types)
    {
        try
        {
            var handler = await _documentEventHandler.Value;
            return await handler.InvokeAsync<long>("addEventListener",
                DotNetObjectReference.Create(invoker), types);
        }
        catch (JSException)
        {
            return 0;
        }
    }

    public async Task<long?> RemoveEventListenerAsync(long? id)
    {
        try
        {
            if (id.HasValue)
            {
                var handler = await _documentEventHandler.Value;
                await handler.InvokeVoidAsync("removeEventListener", id);
            }

            return null;
        }
        catch (JSException)
        {
            return null;
        }
    }
}