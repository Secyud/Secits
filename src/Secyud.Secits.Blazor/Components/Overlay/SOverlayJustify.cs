namespace Secyud.Secits.Blazor;

[SValue]
public partial struct SOverlayJustify
{
    public static SOverlayJustify BeforeBegin { get; } = new("oj-bb");
    public static SOverlayJustify Begin { get; } = new("oj-b");
    public static SOverlayJustify Middle { get; } = new("oj-m");
    public static SOverlayJustify End { get; } = new("oj-e");
    public static SOverlayJustify AfterEnd { get; } = new("oj-ae");
}