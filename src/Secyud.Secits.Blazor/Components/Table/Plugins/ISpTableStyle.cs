using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor.Plugins;

public interface ISpTableStyle : ISPlugin
{
    void BuildClassStyle(ClassStyleContext context);
}