namespace Secyud.Secits.Blazor;

[SValue]
public partial struct SFormGap
{
    public static SFormGap Small { get; } = new("gap-sm");
    public static SFormGap Medium { get; } = new("gap-md");
    public static SFormGap Large { get; } = new("gap-lg");
}