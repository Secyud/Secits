namespace Secyud.Secits.Blazor.JSInterop;

public interface IJsDocument
{
    Task<long> AddEventListenerAsync<TEventArgs>(Func<TEventArgs, Task> func, params string[] types);
    Task<long> AddEventListenerAsync(Func<Task> func, params string[] types);
    Task<long?> RemoveEventListenerAsync(long? id);
}