using Microsoft.JSInterop;

namespace Secyud.Secits.Blazor.JSInterop;

public class Invoker<TEventArgs>(Func<TEventArgs, Task> func)
{
    [JSInvokable("Invoke")]
    public Task Invoke(TEventArgs e)
    {
        return func(e);
    }
}

public class Invoker(Func<Task> func)
{
    [JSInvokable("Invoke")]
    public Task Invoke()
    {
        return func();
    }
}