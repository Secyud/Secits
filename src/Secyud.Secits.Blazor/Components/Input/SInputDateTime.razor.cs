using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Plugins;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInputDateTime<TValue> : EComponentBase<TValue>
    where TValue : IParsable<TValue>
{
    private const string DateFormat = "yyyy-MM-dd"; // Compatible with HTML 'date' inputs
    private const string DateTimeLocalFormat = "yyyy-MM-ddTHH:mm:ss"; // Compatible with HTML 'datetime-local' inputs
    private const string MonthFormat = "yyyy-MM"; // Compatible with HTML 'month' inputs
    private const string TimeFormat = "HH:mm:ss"; // Compatible with HTML 'time' inputs

    private string _typeAttributeValue = null!;
    private string _format = null!;

    /// <summary>
    /// Gets or sets the type of HTML input to be rendered.
    /// </summary>
    [Parameter]
    public InputDateType Type { get; set; } = InputDateType.Date;


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

    protected override void OnParametersSet()
    {
        (_typeAttributeValue, _format) = Type switch
        {
            InputDateType.Date => ("date", DateFormat),
            InputDateType.DateTimeLocal => ("datetime-local", DateTimeLocalFormat),
            InputDateType.Month => ("month", MonthFormat),
            InputDateType.Time => ("time", TimeFormat),
            _ => throw new InvalidOperationException($"Unsupported {nameof(InputDateType)} '{Type}'.")
        };

        _inputValue = string.Format($"{{0:{_format}}}", Value);
    }

    protected override void BuildInputRenderTree(RenderTreeBuilder builder)
    {
        ValidateGeneric();

        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "type", _typeAttributeValue);
        builder.AddAttributeIfNotEmpty(3, "name", Name);
        builder.AddAttributeIfNotEmpty(5, "readonly", GetReadonly());
        builder.AddAttributeIfNotEmpty(6, "disabled", GetDisabled());
        builder.AddAttribute(7, "value", _inputValue);
        builder.AddAttribute(8, "onchange", CreateInputEvent(OnInputAsync, _inputValue));
        builder.CloseElement();
    }

    protected override bool CheckGenericIsValid()
    {
        var type = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);
        return type == typeof(DateTime) ||
               type == typeof(DateTimeOffset) ||
               type == typeof(DateOnly) ||
               type == typeof(TimeOnly);
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