namespace Secyud.Secits.Blazor.Services;

public interface ISecitsService
{
    ValueTask SetCurrentStyle(string style, SecitsThemeParam param);
}