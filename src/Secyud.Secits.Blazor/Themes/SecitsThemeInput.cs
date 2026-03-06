namespace Secyud.Secits.Blazor.Themes;

public class SecitsThemeInput
{
    public Dictionary<string, string> Parameters { get; } = [];
    public bool IsRtl { get; set; }

    public SecitsThemeInput()
    {
        Parameters[SecitsStylesOptions.Color] = "default";
        Parameters[SecitsStylesOptions.Param] = "default";
        Parameters[SecitsStylesOptions.Style] = "default";
    }
}