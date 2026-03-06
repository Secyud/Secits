using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public interface ISecitsService
{
    ValueTask SetCurrentStyle(string style, SecitsThemeInput input);
}