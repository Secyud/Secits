using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor;

public partial class DatePanelYear
{
    protected override string ComponentClass => "s-date-panel-year";
    [Parameter] public EventCallback HeaderClick { get; set; }

    protected Task OnHeaderContentClick(MouseEventArgs args)
    {
        return HeaderClick.InvokeAsync(args);
    }

    protected static DateOnly MaxDate { get; } = new(9989, 12, 31);
    protected static DateOnly MinDate { get; } = new(1910, 1, 1);

    protected override void OnAngleLftClick(MouseEventArgs args)
    {
        if (CurrentShow > MinDate)
        {
            CurrentShow = CurrentShow.AddYears(-10);
            ShowChanged.InvokeAsync(CurrentShow);
        }
    }

    protected override void OnAngleRhtClick(MouseEventArgs args)
    {
        if (CurrentShow < MaxDate)
        {
            CurrentShow = CurrentShow.AddYears(10);
            ShowChanged.InvokeAsync(CurrentShow);
        }
    }
}