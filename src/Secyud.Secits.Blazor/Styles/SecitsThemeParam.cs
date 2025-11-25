namespace Secyud.Secits.Blazor;

public class SecitsThemeParam
{
    public UiThemeColor ThemeColor { get; set; } = UiThemeColor.Default;
    public UiThemeStyle ThemeStyle { get; set; } = UiThemeStyle.Default;
    public UiThemeParam ThemeParam { get; set; } = UiThemeParam.Default;
    public bool IsRtl { get; set; }

    public void MapTo(SecitsThemeParam to)
    {
        to.ThemeColor = ThemeColor;
        to.ThemeParam = ThemeParam;
        to.ThemeStyle = ThemeStyle;
        to.IsRtl = IsRtl;
    }
}