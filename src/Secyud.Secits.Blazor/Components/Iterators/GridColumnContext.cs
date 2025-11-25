using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class GridColumnContext<TValue>(IGridColumnRenderer<TValue> renderer, int sequence)
{
    public int Index { get; set; }
    public int Sequence { get; } = sequence;
    public IGridColumnRenderer<TValue> Renderer { get; } = renderer;
}