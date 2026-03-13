using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Plugins;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

/// <summary>
/// Editor component base for input
/// </summary>
public abstract partial class EComponentBase<TValue> : IActivableComponent, IInputComponent,
    IPluggableComponent, IThemedComponent
{
    // ReSharper disable once StaticMemberInGenericType
    private static bool? _isValid;

    protected void ValidateGeneric()
    {
        _isValid ??= CheckGenericIsValid();
        if (!_isValid.Value)
        {
            throw new InvalidOperationException($"The type '{typeof(TValue)}' is not a supported for {GetType()}.");
        }
    }

    protected virtual bool CheckGenericIsValid() => true;

    protected EComponentBase()
    {
        PluginContext = new SPluginContext(this);
    }

    protected override string ComponentClass => "s-input";

    [Parameter] public bool Disabled { get; set; }
    [Parameter] public bool Readonly { get; set; }
    [Parameter] public string? Name { get; set; }

    [Parameter(CaptureUnmatchedValues = true)]
    public IDictionary<string, object>? AdditionalAttributes { get; set; }

    /// <summary>
    /// <see cref="SInputType"/>
    /// </summary>
    [Parameter]
    public SInputType Type
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            SetDirty();
        }
    }

    [Parameter] public RenderFragment? Plugins { get; set; }
    [Parameter] public TValue Value { get; set; } = default!;
    [Parameter] public EventCallback<TValue> ValueChanged { get; set; }
    [Parameter] public Expression<Func<TValue>>? ValueExpression { get; set; }
    [Parameter] public SColor Color { get; set; }

    public SPluginContext PluginContext { get; }

    private readonly SPluginsContainer<ISpValueHandler<TValue>> _valueHandlers = new();
    private readonly SPluginsContainer<ISpInputElement> _inputElements = new();

    public virtual void ApplyPlugin(ISPlugin plugin)
    {
        _valueHandlers.TryApply(plugin);
        _inputElements.TryApply(plugin);
        StateHasChanged();
    }

    public virtual void ForgoPlugin(ISPlugin plugin)
    {
        _valueHandlers.TryForgo(plugin);
        _inputElements.TryForgo(plugin);
        StateHasChanged();
    }

    public virtual Task TriggerInputChangedEventAsync(string? input)
    {
        return Task.CompletedTask;
    }

    public virtual async Task TriggerValueChangedEventAsync(TValue value)
    {
        await ValueChanged.InvokeAsync(value);
        await _valueHandlers.InvokeAsync(u
            => u.HandleValueAsync(value));
    }


    protected override void ConfigureClassStyle(ClassStyleContext context)
    {
        base.ConfigureClassStyle(context);
        context.AppendClass(Type);
    }

    protected string? GetReadonly()
    {
        return Readonly ? "readonly" : null;
    }

    protected string? GetDisabled()
    {
        return Disabled ? "disabled" : null;
    }

    protected EventCallback<ChangeEventArgs> CreateInputEvent(Action<string?> action, string? inputValue)
    {
        return EventCallback.Factory.CreateBinder<string?>(this, action, inputValue);
    }

    protected EventCallback<ChangeEventArgs> CreateInputEvent(Func<string?, Task> action, string? inputValue)
    {
        return EventCallback.Factory.CreateBinder<string?>(this, action, inputValue);
    }

    protected static Type GetGenericType()
    {
        return Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
    }

    protected abstract void BuildInputRenderTree(RenderTreeBuilder builder);
}