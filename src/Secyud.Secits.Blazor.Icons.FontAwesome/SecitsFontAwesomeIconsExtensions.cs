using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor;

public static class SecitsFontAwesomeIconsExtensions
{
    public static SecitsBlazorBuildContext AddSecitsIconFontAwesome(
        this SecitsBlazorBuildContext context, bool useSecitsFontAwesome = true)
    {
        if (useSecitsFontAwesome)
            context.AddFontAwesome();
        context.Services.AddSingleton<IIconProvider, FontAwesomeIconProvider>();
        return context;
    }
}