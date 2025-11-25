namespace Secyud.Secits.Blazor;

public interface IPluggable
{
    internal void StateHasChanged();

    internal Task InvokeAsync(Action action);

    internal Task InvokeAsync(Func<Task> action);
}