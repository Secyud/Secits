using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor.Services;

public class NullIconProvider : IIconProvider
{
    public string GetIcon(IconName name)
    {
        return "";
    }
}