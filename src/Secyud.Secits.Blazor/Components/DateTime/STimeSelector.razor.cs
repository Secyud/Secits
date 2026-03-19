using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.JsInterop;

namespace Secyud.Secits.Blazor;

public partial class STimeSelector
{
    protected override string ComponentClass => "s-time-selector";
    private ElementReference _hourRef;
    private ElementReference _minuteRef;
    private ElementReference _secondRef;
    private bool _hourNeedScroll;
    private bool _minuteNeedScroll;
    private bool _secondNeedScroll;

    private TimeOnly _time;
    [Inject] public IJSRuntime Js { get; set; } = null!;
    [Parameter] public RenderFragment? HeaderTemplate { get; set; }
    [Parameter] public TimeOnly Time { get; set; }
    [Parameter] public EventCallback<TimeOnly> TimeChanged { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await TryScrollToElementAsync(_hourRef, ref _hourNeedScroll);
        await TryScrollToElementAsync(_minuteRef, ref _minuteNeedScroll);
        await TryScrollToElementAsync(_secondRef, ref _secondNeedScroll);
    }

    protected override void OnParametersSet()
    {
        if (_time != Time)
        {
            _time = Time;
            _hourNeedScroll = true;
            _minuteNeedScroll = true;
            _secondNeedScroll = true;
        }
    }

    protected ValueTask TryScrollToElementAsync(ElementReference e, ref bool scroll)
    {
        if (scroll && e.Context is not null)
        {
            scroll = false;
            return e.ScrollToElement(Js, ".selected");
        }

        return ValueTask.CompletedTask;
    }

    protected async Task OnTimeChanged(TimeOnly time)
    {
        _time = time;
        await TimeChanged.InvokeAsync(time);
    }

    protected async Task OnTimeChanged(TimeSpan span)
    {
        await OnTimeChanged(_time.Add(span));
    }

    protected async Task OnHourChanged(int hour)
    {
        _hourNeedScroll = true;
        await OnTimeChanged(TimeSpan.FromHours(hour - _time.Hour));
    }

    protected async Task OnMinuteChanged(int minute)
    {
        _minuteNeedScroll = true;
        await OnTimeChanged(TimeSpan.FromMinutes(minute - _time.Hour));
    }

    protected async Task OnSecondChanged(int second)
    {
        _secondNeedScroll = true;
        await OnTimeChanged(TimeSpan.FromSeconds(second - _time.Second));
    }
}