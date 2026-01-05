using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.JSInterop;

public class SAppDocument : IAppDocument
{
    public Task ClickAsync(MouseEventArgs args)
    {
        return Click?.Invoke(args) ?? Task.CompletedTask;
    }

    public Task MoveAsync(MouseEventArgs args)
    {
        return Move?.Invoke(args) ?? Task.CompletedTask;
    }

    public Task UpAsync(MouseEventArgs args)
    {
        return Up?.Invoke(args) ?? Task.CompletedTask;
    }

    public Task DownAsync(MouseEventArgs args)
    {
        return Down?.Invoke(args) ?? Task.CompletedTask;
    }

    public Task LeaveAsync(MouseEventArgs args)
    {
        return Leave?.Invoke(args) ?? Task.CompletedTask;
    }

    public event Func<MouseEventArgs, Task>? Click;
    public event Func<MouseEventArgs, Task>? Move;
    public event Func<MouseEventArgs, Task>? Up;
    public event Func<MouseEventArgs, Task>? Down;
    public event Func<MouseEventArgs, Task>? Leave;
}