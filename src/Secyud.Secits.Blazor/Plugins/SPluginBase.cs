using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Plugins;

public abstract class SPluginBase : IComponent, ISPlugin, IDisposable
{
    public abstract string PluginName { get; }

    [CascadingParameter]
    protected SPluginContext? Context
    {
        get;
        set
        {
            if (field == value) return;
            field?.Component.ForgoPlugin(this);
            field = value;
            field?.Component.ApplyPlugin(this);
            OnComponentSet();
        }
    }

    public void Attach(RenderHandle renderHandle)
    {
    }

    public virtual Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        return Task.CompletedTask;
    }

    protected virtual void BuildRenderTree(RenderTreeBuilder builder)
    {
    }

    protected virtual void OnComponentSet()
    {
    }

    protected void SetComponentRef<TComponent>(ref TComponent? component)
        where TComponent : class
    {
        component = Context?.Component as TComponent;
    }

    protected static string? JoinString(List<string?> strList)
    {
        var res = string.Join(' ', strList.Where(u => !string.IsNullOrWhiteSpace(u)));
        return string.IsNullOrWhiteSpace(res) ? null : res;
    }

    protected virtual void Dispose(bool isDisposing)
    {
        Context = null;
    }

    ~SPluginBase()
    {
        Dispose(false);
    }

    void IDisposable.Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}