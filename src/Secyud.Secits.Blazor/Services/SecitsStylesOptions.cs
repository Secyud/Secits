using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public class SecitsStylesOptions
{
    public const string CookieName = "secits-theme";
    public const string RootPath = "_content/Secyud.Secits.Blazor/";
    public const string Color = "secits-theme-color";
    public const string Param = "secits-theme-param";
    public const string Style = "secits-theme-style";

    public List<Func<SecitsThemeInput, IEnumerable<SecitsStyleFile>>> Styles { get; } = [];

    public List<SecitsStyleFile> Get(SecitsThemeInput? input = null)
    {
        input ??= new SecitsThemeInput();
        List<SecitsStyleFile> res = [];
        if (input.Parameters.TryGetValue(Color, out var color))
        {
            res.Add(new SecitsStyleFile(RootPath + $"css/color/{color}.min.css", Color));
        }

        if (input.Parameters.TryGetValue(Param, out var param))
        {
            res.Add(new SecitsStyleFile(RootPath + $"css/param/{param}.min.css", Param));
        }

        if (input.Parameters.TryGetValue(Style, out var style))
        {
            res.Add(new SecitsStyleFile(RootPath + $"css/style/{style}.min.css", Style));
        }

        res.AddRange(Styles.SelectMany(styleFile => styleFile(input)));

        return res;
    }
}