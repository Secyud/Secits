using Microsoft.AspNetCore.Components.Web;

namespace Secyud.Secits.Blazor;

public partial class DatePanelDecade
{
    protected override string ComponentClass => "s-date-panel-decade";
    protected static DateOnly MaxDate { get; } = new(9899, 12, 31);
    protected static DateOnly MinDate { get; } = new(2000, 1, 1);

    protected override void OnAngleLftClick(MouseEventArgs args)
    {
        if (CurrentShow > MinDate)
        {
            CurrentShow = CurrentShow.AddYears(-100);
            ShowChanged.InvokeAsync(CurrentShow);
        }
    }

    protected override void OnAngleRhtClick(MouseEventArgs args)
    {
        if (CurrentShow < MaxDate)
        {
            CurrentShow = CurrentShow.AddYears(100);
            ShowChanged.InvokeAsync(CurrentShow);
        }
    }
}