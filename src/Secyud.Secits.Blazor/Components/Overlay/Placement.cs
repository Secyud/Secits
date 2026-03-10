namespace Secyud.Secits.Blazor;

[SValue]
public partial struct Placement
{
    public static Placement Left { get; } = new("pl-l");
    public static Placement Right { get; } = new("pl-r");
    public static Placement Top { get; } = new("pl-t");
    public static Placement Bottom { get; } = new("pl-b");
}