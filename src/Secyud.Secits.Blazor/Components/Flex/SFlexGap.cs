namespace Secyud.Secits.Blazor;

[SValue]
public partial struct SFlexGap
{
    public static SFlexGap Small { get; } =new("gap-sm");
    public static SFlexGap Medium{ get; } = new("gap-md");
    public static SFlexGap Large { get; } =new("gap-lg");
}