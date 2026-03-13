using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Plugins;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInputDateTime<TValue> : EComponentBase<TValue>
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
    public InputDateType DateType { get; set; } = InputDateType.Date;


    private string? _inputValue;

    private TValue _currentValue = default!;

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
        (_typeAttributeValue, _format) = DateType switch
        {
            InputDateType.Date => ("date", DateFormat),
            InputDateType.DateTimeLocal => ("datetime-local", DateTimeLocalFormat),
            InputDateType.Month => ("month", MonthFormat),
            InputDateType.Time => ("time", TimeFormat),
            _ => throw new InvalidOperationException($"Unsupported {nameof(InputDateType)} '{Type}'.")
        };

        if (!Equals(_currentValue, Value))
        {
            _currentValue = Value;
            _inputValue = FormatValueAsString(Value);
        }
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
        var type = GetGenericType();
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
        if (TryParseValueFromString(input, out var value))
        {
            _inputValue = input;
            await TriggerValueChangedEventAsync(value);
        }
    }

    protected string FormatValueAsString(TValue? value)
        => value switch
        {
            DateTime dateTimeValue => BindConverter.FormatValue(dateTimeValue, _format, CultureInfo.InvariantCulture),
            DateTimeOffset dateTimeOffsetValue => BindConverter.FormatValue(dateTimeOffsetValue, _format,
                CultureInfo.InvariantCulture),
            DateOnly dateOnlyValue => BindConverter.FormatValue(dateOnlyValue, _format, CultureInfo.InvariantCulture),
            TimeOnly timeOnlyValue => BindConverter.FormatValue(timeOnlyValue, _format, CultureInfo.InvariantCulture),
            _ => string.Empty, // Handles null for Nullable<DateTime>, etc.
        };

    protected bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result)
    {
        return BindConverter.TryConvertTo(value, CultureInfo.InvariantCulture, out result);
    }
}