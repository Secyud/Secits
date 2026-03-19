using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor;

public partial class DatePanelMonth
{
    protected override string ComponentClass => "s-date-panel-month";
    [Parameter] public EventCallback HeaderClick { get; set; }


    protected Task OnHeaderContentClick(MouseEventArgs args)
    {
        return HeaderClick.InvokeAsync(args);
    }

    protected static DateOnly MaxDate { get; } = new(9998, 12, 31);
    protected static DateOnly MinDate { get; } = new(1901, 1, 1);
    protected override void OnAngleLftClick(MouseEventArgs args)
    {
        if (CurrentShow > MinDate)
        {
            CurrentShow = CurrentShow.AddYears(-1);
            ShowChanged.InvokeAsync(CurrentShow);
        }
    }

    protected override void OnAngleRhtClick(MouseEventArgs args)
    {
        if (CurrentShow < MaxDate)
        {
            CurrentShow = CurrentShow.AddYears(1);
            ShowChanged.InvokeAsync(CurrentShow);
        }
    }
}