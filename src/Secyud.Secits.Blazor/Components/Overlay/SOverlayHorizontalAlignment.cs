namespace Secyud.Secits.Blazor;

[SValue]
public partial struct SOverlayHorizontalAlignment
{
    public static SOverlayHorizontalAlignment OuterLeft { get; } = new("o-ol");
    public static SOverlayHorizontalAlignment InnerLeft { get; } = new("o-il");
    public static SOverlayHorizontalAlignment Center { get; } = new("o-ct");
    public static SOverlayHorizontalAlignment InnerRight { get; } = new("o-ir");
    public static SOverlayHorizontalAlignment OuterRight { get; } = new("o-or");
}