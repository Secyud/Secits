using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor;

public abstract partial class DatePanelBase
{
    protected override string ComponentClass => "s-date-panel";
    [Parameter] public DateOnly Show { get; set; }
    [Parameter] public EventCallback<DateOnly> ShowChanged { get; set; }
    [Parameter] public DateOnly Date { get; set; }
    [Parameter] public EventCallback<DateOnly> DateChanged { get; set; }

    protected DateOnly CurrentShow { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    protected DateOnly CurrentDate { get; set; }

    protected override void OnParametersSet()
    {
        CurrentShow = Show;
        CurrentDate = Date;
    }

    protected abstract void OnAngleLftClick(MouseEventArgs args);

    protected abstract void OnAngleRhtClick(MouseEventArgs args);

    protected async Task OnDateChanged(DateOnly date)
    {
        CurrentDate = date;
        CurrentShow = date;
        await DateChanged.InvokeAsync(date);
    }
}