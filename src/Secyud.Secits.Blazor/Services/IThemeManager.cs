using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public interface IThemeManager
{
    ValueTask SetCurrentStyle(string style, SecitsThemeInput input);
}