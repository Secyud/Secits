using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class SDynamicComponentContext(string key, RenderFragment renderFragment)
{
    public string Key { get; } = key;
    public RenderFragment RenderFragment { get; } = renderFragment;


    public event Action? StateHasChangedEvent;

    public void StateHasChanged()
    {
        StateHasChangedEvent?.Invoke();
    }

    public required Func<bool, Task> OnAfterRenderAsyncEvent { get; set; }
    public required Action<bool> OnAfterRenderEvent { get; set; }
    public required Func<Task> OnInitializedAsyncEvent { get; set; }
    public required Action OnInitializedEvent { get; set; }
}