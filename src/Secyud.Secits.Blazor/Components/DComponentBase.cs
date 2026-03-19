using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Themes;

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
        _dirtyParameters = new Lazy<IReadOnlyList<IDirtyParameter>>(() =>
            DirtyParameterProvider.GetDirtyParameters(this));
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
        if (_isInitialized)
        {
            if (!_isDirty)
            {
                foreach (var dirtyParameter in _dirtyParameters.Value)
                {
                    if (dirtyParameter.CheckComponentDirty(this, parameters))
                    {
                        SetDirty();
                        break;
                    }
                }
            }
        }
        else
        {
            _isInitialized = true;
            AppContext.CreateDynamicComponent(Context);
        }

        parameters.SetParameterProperties(this);

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

    protected Task InvokeAsync(Action action)
    {
        return Context.InvokeAsync(action);
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


    [Inject] private IDirtyParameterProvider DirtyParameterProvider { get; set; } = null!;

    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Style { get; set; }

    public ElementReference ElementRef { get; protected set; }

    protected virtual string? ComponentClass => null;

    private readonly Lazy<IReadOnlyList<IDirtyParameter>> _dirtyParameters;
    private string? _builtClass;
    private string? _builtStyle;
    private bool _isDirty = true;


    public void SetDirty()
    {
        _isDirty = true;
    }


    /// <summary>
    /// 生成样式和类
    /// </summary>
    private void GenerateClassAndStyle()
    {
        if (!_isDirty) return;
        var context = new ClassStyleContext();

        ConfigureClassStyle(context);

        foreach (var dirtyParameter in _dirtyParameters.Value)
        {
            dirtyParameter.BuildComponentClassStyle(this, context);
        }

        var cls = context.ClassBuilder.ToString();
        _builtClass = string.IsNullOrWhiteSpace(cls) ? null : cls;
        var stl = context.StyleBuilder.ToString();
        _builtStyle = string.IsNullOrWhiteSpace(stl) ? null : stl;
        _isDirty = false;
    }

    /// <summary>
    /// 部分组件有自己的样式逻辑
    /// </summary>
    /// <param name="context"></param>
    protected virtual void ConfigureClassStyle(ClassStyleContext context)
    {
        context.AppendClass(ComponentClass);
    }

    protected string? GetClass()
    {
        GenerateClassAndStyle();
        return _builtClass;
    }

    protected string? GetStyle()
    {
        GenerateClassAndStyle();
        return _builtStyle;
    }
}