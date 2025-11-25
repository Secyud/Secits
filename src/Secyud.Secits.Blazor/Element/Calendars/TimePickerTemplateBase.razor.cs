using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Element;

public abstract partial class TimePickerTemplateBase : IHasValue<TimeOnly>
{
    [Parameter]
    public TimeOnly Value { get; set; }

    [Parameter]
    public EventCallback<TimeOnly> ValueChanged { get; set; }

    [Parameter]
    public TimePrecision Pression { get; set; }

    protected int GetHour()
    {
        return CurrentValue.Hour;
    }

    protected Task SetHourAsync(int value)
    {
        value = (value + 24) % 24;
        return SetValueAsync(hour: value);
    }

    protected int GetMinute()
    {
        return CurrentValue.Minute;
    }

    protected Task SetMinuteAsync(int value)
    {
        value = (value + 60) % 60;
        return SetValueAsync(minute: value);
    }

    protected int GetSecond()
    {
        return CurrentValue.Second;
    }

    protected Task SetSecondAsync(int value)
    {
        value = (value + 60) % 60;
        return SetValueAsync(second: value);
    }

    protected TimeOnly CurrentValue => Value;

    protected async Task SetValueAsync(int? hour = null, int? minute = null, int? second = null)
    {
        if (ValueChanged.HasDelegate)
        {
            var time = new TimeOnly(
                hour ?? CurrentValue.Hour,
                minute ?? CurrentValue.Minute,
                second ?? CurrentValue.Second);
            await ValueChanged.InvokeAsync(time);
        }
    }
}