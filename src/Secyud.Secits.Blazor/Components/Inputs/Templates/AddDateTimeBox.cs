using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor;

public class AddDateTimeBox<TValue> : AddTemplateBase<TValue>
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

    /// <summary>
    /// Constructs an instance of <see cref="InputDate{TValue}"/>
    /// </summary>
    public AddDateTimeBox()
    {
        var type = Nullable.GetUnderlyingType(typeof(TValue)) ?? typeof(TValue);

        if (type != typeof(DateTime) &&
            type != typeof(DateTimeOffset) &&
            type != typeof(DateOnly) &&
            type != typeof(TimeOnly))
        {
            throw new InvalidOperationException($"Unsupported {GetType()} type param '{type}'.");
        }
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        (_typeAttributeValue, _format) = Type switch
        {
            InputDateType.Date => ("date", DateFormat),
            InputDateType.DateTimeLocal => ("datetime-local", DateTimeLocalFormat),
            InputDateType.Month => ("month", MonthFormat),
            InputDateType.Time => ("time", TimeFormat),
            _ => throw new InvalidOperationException($"Unsupported {nameof(InputDateType)} '{Type}'.")
        };
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, "input");
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "type", _typeAttributeValue);
        builder.AddAttributeIfNotEmpty(3, "name", Master.Name);
        builder.AddAttribute(3, "class", GetClass());
        builder.AddAttribute(4, "style", GetStyle());
        builder.AddAttributeIfNotEmpty(5, "readonly", GetReadonly());
        builder.AddAttributeIfNotEmpty(6, "disabled", GetDisabled());
        builder.AddAttribute(7, "value", InputString);
        builder.AddAttribute(8, "onchange",
            EventCallback.Factory.CreateBinder<string?>(this, OnChangeAsync, InputString));
        builder.AddAttribute(9, "oninput",
            EventCallback.Factory.CreateBinder<string?>(this, OnInputAsync, InputString));
        builder.CloseElement();
    }

    /// <inheritdoc />
    protected override string FormatValueAsString(TValue? value) =>
        value switch
        {
            DateTime dateTimeValue => BindConverter.FormatValue(dateTimeValue, _format, CultureInfo.InvariantCulture),
            DateTimeOffset dateTimeOffsetValue => BindConverter.FormatValue(dateTimeOffsetValue, _format, CultureInfo.InvariantCulture),
            DateOnly dateOnlyValue => BindConverter.FormatValue(dateOnlyValue, _format, CultureInfo.InvariantCulture),
            TimeOnly timeOnlyValue => BindConverter.FormatValue(timeOnlyValue, _format, CultureInfo.InvariantCulture),
            _ => string.Empty, // Handles null for Nullable<DateTime>, etc.
        };

    /// <inheritdoc />
    protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out TValue result)
    {
        if (BindConverter.TryConvertTo(value, CultureInfo.InvariantCulture, out result))
        {
            Debug.Assert(result != null);
            return true;
        }

        return false;
    }
}