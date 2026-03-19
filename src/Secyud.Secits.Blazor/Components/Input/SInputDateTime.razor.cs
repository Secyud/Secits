using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.Plugins;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInputDateTime<TValue> : EComponentBase<TValue>
{
    public SInputDateTime()
    {
        _genericType = GetGenericType();
        _isNullable = Nullable.GetUnderlyingType(typeof(TValue)) is not null;
    }
    protected override string ComponentClass => "s-input-date-time";

    private const string DateFormat = "yyyy-MM-dd"; // Compatible with HTML 'date' inputs
    private const string DateTimeLocalFormat = "yyyy-MM-ddTHH:mm:ss"; // Compatible with HTML 'datetime-local' inputs
    private const string MonthFormat = "yyyy-MM"; // Compatible with HTML 'month' inputs
    private const string TimeFormat = "HH:mm:ss"; // Compatible with HTML 'time' inputs

    private string _typeAttributeValue = null!;
    private string _format = null!;
    private readonly Type _genericType;
    private readonly bool _isNullable;

    protected bool OverlayVisible { get; set; }

    /// <summary>
    /// Gets or sets the type of HTML input to be rendered.
    /// </summary>
    [Parameter]
    public SInputDateType DateType { get; set; } = SInputDateType.Date;
    [Parameter]
    public SOverlayControlType ControlType { get; set; } 


    private string? _inputValue;

    private DateOnly? _currentDate;
    private TimeOnly? _currentTime;

    private DateOnly? CurrentDate
    {
        get => _currentDate;
        set
        {
            _currentDate = value;
            OnSelectAsync().ConfigureAwait(false);
        }
    }

    private TimeOnly? CurrentTime
    {
        get => _currentTime;
        set
        {
            _currentTime = value;
            OnSelectAsync().ConfigureAwait(false);
        }
    }

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
            SInputDateType.Date => ("date", DateFormat),
            SInputDateType.DateTimeLocal => ("datetime-local", DateTimeLocalFormat),
            SInputDateType.Month => ("month", MonthFormat),
            SInputDateType.Time => ("time", TimeFormat),
            _ => throw new InvalidOperationException($"Unsupported {nameof(SInputDateType)} '{Type}'.")
        };

        SetCurrentValue(Value);
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

    protected void SetCurrentValue(object? value)
    {
        if (!Equals(CurrentValue, value))
        {
            CurrentValue = Value;
            _inputValue = FormatValueAsString((TValue?)value);
        }
    }


    protected override bool CheckGenericIsValid()
    {
        var type = GetGenericType();
        return type == typeof(DateTime) ||
               type == typeof(DateTimeOffset) ||
               type == typeof(DateOnly) ||
               type == typeof(TimeOnly);
    }

    protected virtual async Task OnSelectAsync()
    {
        object? obj = null;
        if (_genericType == typeof(DateOnly))
        {
            obj = _isNullable ? _currentDate : _currentDate ?? default(DateOnly);
        }
        else if (_genericType == typeof(TimeOnly))
        {
            obj = _isNullable ? _currentTime : _currentTime ?? default(TimeOnly);
        }
        else
        {
            DateTime? dateTime = _currentTime is null && _currentDate is null
                ? null
                : new DateTime(_currentDate ?? default, _currentTime ?? default);
            if (_genericType == typeof(DateTimeOffset))
            {
                DateTimeOffset? offset = dateTime is null ? null : new DateTimeOffset(dateTime.Value);
                obj = _isNullable ? offset : offset ?? default(DateTimeOffset);
            }
            else if (_genericType == typeof(DateTime))
            {
                obj = _isNullable ? dateTime : dateTime ?? default(DateTime);
            }
        }

        var value = (TValue?)obj;
        _inputValue = FormatValueAsString(value);
        await TriggerValueChangedEventAsync(value);
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

    protected void OnCalendarClickAsync(MouseEventArgs args)
    {
        OverlayVisible = !OverlayVisible;
        if (!OverlayVisible) return;
        if (CurrentValue is null)
        {
            _currentDate = null;
            _currentTime = null;
            return;
        }

        if (_genericType == typeof(DateOnly))
        {
            _currentDate = (DateOnly)(object)CurrentValue;
            _currentTime = null;
        }
        else if (_genericType == typeof(TimeOnly))
        {
            _currentDate = null;
            _currentTime = (TimeOnly)(object)CurrentValue;
        }
        else
        {
            DateTime dateTime = default;
            if (_genericType == typeof(DateTimeOffset))
            {
                var offset = (DateTimeOffset)(object)CurrentValue;
                dateTime = offset.DateTime;
            }
            else if (_genericType == typeof(DateTime))
            {
                dateTime = (DateTime)(object)CurrentValue;
            }

            _currentDate = DateOnly.FromDateTime(dateTime);
            _currentTime = TimeOnly.FromDateTime(dateTime);
        }
    }
}