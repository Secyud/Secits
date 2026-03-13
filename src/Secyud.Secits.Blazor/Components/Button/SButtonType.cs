namespace Secyud.Secits.Blazor;

[SValue]
public partial struct SButtonType
{
    public static SButtonType Text { get; } = "text";
    public static SButtonType Outline { get; } = "outline";
    public static SButtonType Plain { get; } = "plain";
    public static SButtonType Shadow { get; } = "shadow";
}