using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor;

/// <summary>
/// dynamic component base, render component in app container
/// </summary>
public class DComponentBase : IComponent, IDisposable, IHandleEvent
{
    private bool _isInitialized;

    public DComponentBase()
    {
        Context = new SDynamicComponentContext(Guid.NewGuid().ToString("N"), BuildRenderTree)
        {
            OnAfterRenderAsyncEvent = OnAfterRenderAsync,
            OnInitializedAsyncEvent = OnInitializedAsync,
            OnInitializedEvent = OnInitialized,
            OnAfterRenderEvent = OnAfterRender,
        };
    }

    [Inject] protected IAppContext AppContext { get; set; } = null!;
    protected SDynamicComponentContext Context { get; }

    protected virtual void OnInitialized()
    {
    }

    protected virtual Task OnInitializedAsync()
    {
        return Task.CompletedTask;
    }

    protected virtual void OnAfterRender(bool firstRender)
    {
    }

    protected virtual Task OnAfterRenderAsync(bool firstRender)
    {
        return Task.CompletedTask;
    }

    public void Attach(RenderHandle renderHandle)
    {
    }

    public virtual Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        if (!_isInitialized)
        {
            _isInitialized = true;
            AppContext.CreateDynamicComponent(Context);
        }

        return Task.CompletedTask;
    }

    protected virtual void BuildRenderTree(RenderTreeBuilder builder)
    {
    }

    protected virtual void Dispose(bool isDisposing)
    {
        if (isDisposing)
        {
            if (_isInitialized)
            {
                AppContext.DeleteDynamicComponent(Context);
            }
        }
    }

    ~DComponentBase()
    {
        Dispose(false);
    }

    protected void StateHasChanged()
    {
        Context.StateHasChanged();
    }

    private async Task CallStateHasChangedOnAsyncCompletion(Task task)
    {
        try
        {
            await task;
        }
        catch
        {
            if (task.IsCanceled)
                return;
            throw;
        }

        StateHasChanged();
    }

    Task IHandleEvent.HandleEventAsync(EventCallbackWorkItem callback, object? arg)
    {
        Task task = callback.InvokeAsync(arg);
        int num = task.Status == TaskStatus.RanToCompletion ? 0 : (task.Status != TaskStatus.Canceled ? 1 : 0);
        StateHasChanged();
        return num == 0 ? Task.CompletedTask : this.CallStateHasChangedOnAsyncCompletion(task);
    }

    void IDisposable.Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}