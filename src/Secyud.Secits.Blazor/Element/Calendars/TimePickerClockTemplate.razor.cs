using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.JSInterop;

namespace Secyud.Secits.Blazor.Element;

public partial class TimePickerClockTemplate
{
    private TimePrecision _clockState = TimePrecision.Default;

    private DateTimePrecisionKind State => _clockState.PrecisionKind;

    [Inject]
    private IJsElement Element { get; set; } = null!;

    private ElementReference _ref;

    private async Task<(double, double)> GetPoint(MouseEventArgs e)
    {
        var rect = await Element.GetBoundingClientRect(_ref);
        var centerX = (rect.Left + rect.Right) / 2;
        var centerY = (rect.Top + rect.Bottom) / 2;
        var pointX = e.ClientX - centerX;
        var pointY = e.ClientY - centerY;
        return (pointX, pointY);
    }

    private double GetMouseAngle((double, double) pointer)
    {
        var (x, y) = pointer;
        return Math.Atan2(x, -y) * 180 / Math.PI + 360;
    }

    private bool GetMouseDistance((double, double) pointer)
    {
        var (x, y) = pointer;
        var distance = x * x + y * y;
        return distance < 32 * 32;
    }

    private async Task OnMouseMove(MouseEventArgs e)
    {
        switch (_clockState.PrecisionKind)
        {
            case DateTimePrecisionKind.Default:
                break;
            case DateTimePrecisionKind.Hour:
            {
                var pointer = await GetPoint(e);
                var angle = GetMouseAngle(pointer);
                var inner = GetMouseDistance(pointer);
                var hour = (int)Math.Round(angle / 30) % 12;
                if (hour == 0) hour = 12;
                await SetHourAsync(hour + (inner ? 0 : 12));
            }
                break;
            case DateTimePrecisionKind.Minute:
            {
                var pointer = await GetPoint(e);
                var angle = GetMouseAngle(pointer);
                await SetMinuteAsync((int)Math.Round(angle / 6));
            }
                break;
            case DateTimePrecisionKind.Second:
            {
                var pointer = await GetPoint(e);
                var angle = GetMouseAngle(pointer);
                await SetSecondAsync((int)Math.Round(angle / 6));
            }
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnMouseDownHour(MouseEventArgs e)
    {
        _clockState = TimePrecision.Hour;
    }

    private void OnMouseDownMinute(MouseEventArgs e)
    {
        _clockState = TimePrecision.Minute;
    }

    private void OnMouseDownSecond(MouseEventArgs e)
    {
        _clockState = TimePrecision.Second;
    }

    private void OnMouseUp()
    {
        _clockState = TimePrecision.Default;
        StateHasChanged();
    }

    private bool HoverInner => GetHour() is <= 12 and > 0;
}