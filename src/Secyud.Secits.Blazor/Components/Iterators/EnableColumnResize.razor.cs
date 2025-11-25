using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Secyud.Secits.Blazor.JSInterop;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableColumnResize<TValue> : IGridHeaderRenderer
{
    [Inject]
    private IJsDocument JsDocument { get; set; } = null!;

    [Parameter]
    public bool FitNextColumn { get; set; }

    private bool _isDrag;
    private long? _dragEventId;
    private long? _endEventId;
    private int _currentColumnIndex;

    protected override void ApplySetting()
    {
        Master.TableHeaders.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.TableHeaders.Forgo(this);
    }

    private async Task SetDragAsync(bool isDrag)
    {
        if (isDrag == _isDrag) return;
        _isDrag = isDrag;
        if (_isDrag)
        {
            _dragEventId = await JsDocument.AddEventListenerAsync<MouseEventArgs>(OnDragColumnHeader, "mousemove");
            _endEventId = await JsDocument.AddEventListenerAsync(EndDragColumnHeader, "mouseup", "mouseleave");
        }
        else
        {
            _dragEventId = await JsDocument.RemoveEventListenerAsync(_dragEventId);
            _endEventId = await JsDocument.RemoveEventListenerAsync(_endEventId);
        }
    }

    private async Task BeginDragColumnHeader(int index)
    {
        await SetDragAsync(true);
        _currentColumnIndex = index;
    }

    private async Task OnDragColumnHeader(MouseEventArgs args)
    {
        if (!_isDrag) return;
        var columns = Master.TableColumns;
        if (_currentColumnIndex >= columns.Count) return;

        var current = columns[_currentColumnIndex];
        var move = (int)args.MovementX;
        if (FitNextColumn && _currentColumnIndex + 1 < columns.Count)
        {
            var next = columns[_currentColumnIndex + 1];
            var currentWidth = current.RealWidth;
            var nextWidth = next.RealWidth;
            var minValue = Math.Max(
                current.MinWidth - currentWidth,
                nextWidth - next.MaxWidth);
            var maxValue = Math.Min(
                current.MaxWidth - currentWidth,
                nextWidth - next.MinWidth);
            move = Math.Clamp(move, minValue, maxValue);
            current.RealWidth += move;
            next.RealWidth -= move;
        }
        else
        {
            current.RealWidth += move;
        }

        await Master.SetDirtyAsync();
    }

    private async Task EndDragColumnHeader()
    {
        await SetDragAsync(false);
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await SetDragAsync(false);
    }
}