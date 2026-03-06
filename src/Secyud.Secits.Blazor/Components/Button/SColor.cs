namespace Secyud.Secits.Blazor;

[SValue]
public partial struct SColor
{
    public static SColor Default { get; } = "";
    public static SColor Primary { get; } = "primary";
    public static SColor Secondary { get; } = "secondary";
    public static SColor Naive { get; } = "naive";
    public static SColor Success { get; } = "success";
    public static SColor Info { get; } = "info";
    public static SColor Warning { get; } = "warning";
    public static SColor Danger { get; } = "danger";
}