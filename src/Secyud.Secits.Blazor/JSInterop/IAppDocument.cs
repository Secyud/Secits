using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor.JSInterop;

public interface IAppDocument
{
    event Func<MouseEventArgs, Task>? Click;
    event Func<MouseEventArgs, Task>? Move;
    event Func<MouseEventArgs, Task>? Up;
    event Func<MouseEventArgs, Task>? Down;
    event Func<MouseEventArgs, Task>? Leave;

    Task ClickAsync(MouseEventArgs args);

    Task MoveAsync(MouseEventArgs args);

    Task UpAsync(MouseEventArgs args);

    Task DownAsync(MouseEventArgs args);

    Task LeaveAsync(MouseEventArgs args);
}