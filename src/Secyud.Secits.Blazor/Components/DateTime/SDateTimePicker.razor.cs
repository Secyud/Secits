using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public partial class SDateTimePicker : IThemedComponent, ISizedComponent
{
    protected override string ComponentClass => "s-datetime-picker";

    [Parameter]
    public SInputDateType DateType
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            CurrentType = field switch
            {
                SInputDateType.Date => DateTimePickerType.Year,
                SInputDateType.DateTimeLocal => DateTimePickerType.Year,
                SInputDateType.Month => DateTimePickerType.Year,
                SInputDateType.Time => DateTimePickerType.Time,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    } = SInputDateType.Date;

    [Parameter] public DateTimePickerType? Type { get; set; }
    [Parameter] public DateOnly? Date { get; set; }
    [Parameter] public EventCallback<DateOnly?> DateChanged { get; set; }
    [Parameter] public TimeOnly? Time { get; set; }
    [Parameter] public EventCallback<TimeOnly?> TimeChanged { get; set; }
    [Parameter] public EventCallback FinishEdit { get; set; }
    [Parameter] public SColor Color { get; set; }
    [Parameter] public SSize Size { get; set; }

    protected DateOnly CurrentShowDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    protected DateOnly CurrentDate { get; set; }
    protected TimeOnly CurrentTime { get; set; }

    protected DateTimePickerType CurrentType
    {
        get;
        set
        {
            if (DateType == SInputDateType.Date && value == DateTimePickerType.Time ||
                DateType == SInputDateType.Month && value is DateTimePickerType.Time or DateTimePickerType.Day)
            {
                FinishEdit.InvokeAsync().ConfigureAwait(false);
                return;
            }

            field = value;
        }
    }

    protected override void OnParametersSet()
    {
        if (Date.HasValue) CurrentDate = Date.Value;
        if (Time.HasValue) CurrentTime = Time.Value;
        if (Type.HasValue) CurrentType = Type.Value;
    }

    protected async Task OnShowChanged(DateOnly date)
    {
        CurrentShowDate = date;
        await InvokeAsync(StateHasChanged);
    }

    protected async Task OnDateChanged(DateOnly date, DateTimePickerType type)
    {
        CurrentType = type;
        CurrentShowDate = date;
        CurrentDate = date;
        await DateChanged.InvokeAsync(date);
    }

    protected async Task SetToCurrent()
    {
        var now = DateTime.Now;
        switch (DateType)
        {
            case SInputDateType.Date:
                await OnDateChanged(DateOnly.FromDateTime(now), DateTimePickerType.Day);
                break;
            case SInputDateType.DateTimeLocal:
                await OnDateChanged(DateOnly.FromDateTime(now), DateTimePickerType.Time);
                await OnTimeChanged(TimeOnly.FromDateTime(now));
                break;
            case SInputDateType.Month:
                await OnDateChanged(new DateOnly(now.Year, now.Month, 1), DateTimePickerType.Month);
                break;
            case SInputDateType.Time:
                await OnTimeChanged(TimeOnly.FromDateTime(now));
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    protected async Task OnTimeChanged(TimeOnly time)
    {
        CurrentTime = time;
        await TimeChanged.InvokeAsync(time);
        await FinishEdit.InvokeAsync();
    }
}