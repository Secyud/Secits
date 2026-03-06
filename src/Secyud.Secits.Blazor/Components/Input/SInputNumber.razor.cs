using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Plugins;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInputNumber<TValue> : EComponentBase<TValue>
    where TValue : IParsable<TValue>
{
    private string? _inputValue;
    private readonly SPluginContainer<ISpInputHandler> _inputHandler = new();

    public override void ApplyPlugin(ISPlugin plugin)
    {
        base.ApplyPlugin(plugin);
        _inputHandler.TryApply(plugin);
    }

    public override void ForgoPlugin(ISPlugin plugin)
    {
        base.ForgoPlugin(plugin);
        _inputHandler.TryForgo(plugin);
    }

    protected override void BuildInputRenderTree(RenderTreeBuilder builder)
    {
        ValidateGeneric();

        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "type", "number");
        builder.AddAttributeIfNotEmpty(3, "name", Name);
        builder.AddAttributeIfNotEmpty(4, "readonly", GetReadonly());
        builder.AddAttributeIfNotEmpty(5, "disabled", GetDisabled());
        builder.AddAttribute(6, "value", _inputValue);
        builder.AddAttribute(7, "oninput", CreateInputEvent(OnInputAsync, _inputValue));
        builder.CloseElement();
    }

    protected override bool CheckGenericIsValid()
    {
        var targetType = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
        return targetType == typeof(int) ||
               targetType == typeof(long) ||
               targetType == typeof(short) ||
               targetType == typeof(float) ||
               targetType == typeof(double) ||
               targetType == typeof(decimal);
    }

    protected override void OnParametersSet()
    {
        _inputValue = Value.ToString();
    }

    protected virtual async Task OnInputAsync(string? str)
    {
        _inputValue = str;
        await _inputHandler.InvokeAsync(
            u => u.HandleInputAsync(str),
            () => TriggerInputChangedEventAsync(str));
    }

    public override async Task TriggerInputChangedEventAsync(string? input)
    {
        if (TValue.TryParse(input, CultureInfo.InvariantCulture, out var value))
            await TriggerValueChangedEventAsync(value);
    }
}