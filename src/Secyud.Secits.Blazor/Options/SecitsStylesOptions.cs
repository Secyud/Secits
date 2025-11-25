namespace Secyud.Secits.Blazor.Options;

public class SecitsStylesOptions
{
    public const string CookieName = "secits-theme";
    public const string RootPath = "_content/Secyud.Secits.Blazor/";
    public const string Color = "secits-theme-color";
    public const string Param = "secits-theme-param";
    public const string Style = "secits-theme-style";

    public List<Func<SecitsThemeParam, IEnumerable<SecitsStyleFile>>> Styles { get; } = [];

    public List<SecitsStyleFile> Get(SecitsThemeParam? param = null)
    {
        param ??= new SecitsThemeParam();
        List<SecitsStyleFile> res = [];
        switch (param.ThemeColor)
        {
            case UiThemeColor.Default:
                res.Add(new SecitsStyleFile(RootPath + "css/color/default.min.css", Color));
                break;
            case UiThemeColor.Dark:
                res.Add(new SecitsStyleFile(RootPath + "css/color/dark.min.css", Color));
                break;
        }

        switch (param.ThemeParam)
        {
            case UiThemeParam.Default:
                res.Add(new SecitsStyleFile(RootPath + "css/param/default.min.css", Param));
                break;
        }

        switch (param.ThemeStyle)
        {
            case UiThemeStyle.Default:
                res.Add(new SecitsStyleFile(RootPath + "css/style/default.min.css", Style));
                break;
        }

        foreach (var style in Styles)
        {
            foreach (var str in style(param))
            {
                res.Add(str);
            }
        }

        return res;
    }
}