namespace Secyud.Secits.Blazor;

[SValue]
public partial struct SOverlayAlignment
{
    public static SOverlayAlignment Top { get; } = new("oa-t");
    public static SOverlayAlignment Left { get; } = new("oa-l");
    public static SOverlayAlignment Right { get; } = new("oa-r");
    public static SOverlayAlignment Bottom { get; } = new("oa-b");
}