using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Element;

public abstract class SElementBase : IComponent
{
    private RenderHandle _renderHandle;
    private bool _hasPendingQueuedRender;
    protected ElementReference? Ref { get; set; }
    public ElementReference? ElementRef => Ref;

    private void RenderFragment(RenderTreeBuilder builder)
    {
        _hasPendingQueuedRender = false;
        BuildRenderTree(builder);
    }

    protected virtual void BuildRenderTree(RenderTreeBuilder builder)
    {
    }

    protected void StateHasChanged()
    {
        if (_hasPendingQueuedRender)
            return;

        _hasPendingQueuedRender = true;
        _renderHandle.Render(RenderFragment);
    }

    protected Task InvokeAsync(Action action) => _renderHandle.Dispatcher.InvokeAsync(action);

    protected Task InvokeAsync(Func<Task> action) => _renderHandle.Dispatcher.InvokeAsync(action);

    public void Attach(RenderHandle renderHandle) => _renderHandle = renderHandle;

    public virtual Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        StateHasChanged();
        return Task.CompletedTask;
    }
}