namespace Secyud.Secits.Blazor;

[SValue]
public partial struct SOverlayVerticalAlignment
{
    public static SOverlayVerticalAlignment OuterTop { get; } = new("o-ot");
    public static SOverlayVerticalAlignment InnerTop { get; } = new("o-it");
    public static SOverlayVerticalAlignment Middle { get; } = new("o-md");
    public static SOverlayVerticalAlignment InnerBottom { get; } = new("o-ib");
    public static SOverlayVerticalAlignment OuterBottom { get; } = new("o-ob");
}