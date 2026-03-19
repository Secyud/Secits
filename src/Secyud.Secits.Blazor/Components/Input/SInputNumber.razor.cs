using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.Plugins;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInputNumber<TValue> : EComponentBase<TValue>
{
    protected override string ComponentClass => "s-input-number";
    private ElementReference _input;
    private string? _inputValue;
    private readonly SPluginContainer<ISpInputHandler> _inputHandler = new();

    [Inject] private IJSRuntime Js { get; set; } = null!;

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
        builder.AddElementReferenceCapture(9, u => _input = u);
        builder.CloseElement();
    }

    protected async Task StepUp()
    {
        await Js.InvokeVoidAsync(SJsModules.Input.NumberStepUp, _input);
    }

    protected async Task StepDown()
    {
        await Js.InvokeVoidAsync(SJsModules.Input.NumberStepDown, _input);
    }

    protected override bool CheckGenericIsValid()
    {
        var type = GetGenericType();
        return type == typeof(int) ||
               type == typeof(long) ||
               type == typeof(short) ||
               type == typeof(float) ||
               type == typeof(double) ||
               type == typeof(decimal);
    }

    protected override void OnParametersSet()
    {
        if (!Equals(CurrentValue, Value))
        {
            CurrentValue = Value;
            _inputValue = FormatValueAsString(Value);
        }
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
        if (TryParseValueFromString(input, out var value))
        {
            _inputValue = input;
            await TriggerValueChangedEventAsync(value);
        }
    }


    protected bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result)
    {
        return BindConverter.TryConvertTo(value, CultureInfo.InvariantCulture, out result);
    }

    protected string? FormatValueAsString(TValue? value)
    {
        return value switch
        {
            null => null,
            int @int => BindConverter.FormatValue(@int, CultureInfo.InvariantCulture),
            long @long => BindConverter.FormatValue(@long, CultureInfo.InvariantCulture),
            short @short => BindConverter.FormatValue(@short, CultureInfo.InvariantCulture),
            float @float => BindConverter.FormatValue(@float, CultureInfo.InvariantCulture),
            double @double => BindConverter.FormatValue(@double, CultureInfo.InvariantCulture),
            decimal @decimal => BindConverter.FormatValue(@decimal, CultureInfo.InvariantCulture),
            _ => throw new InvalidOperationException($"Unsupported type {value.GetType()}")
        };
    }
}