using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor;

public static class SecitsFontAwesomeIconsExtensions
{
    public static SecitsBlazorBuildContext AddFontAwesome(this SecitsBlazorBuildContext context)
    {
        const string cssFile = "_content/Secyud.Secits.Blazor.FontAwesome/css/all.min.css";
        context.Services.Configure<SecitsOptions>(options =>
        {
            if (!options.ExtendStyles.Contains(cssFile))
                options.ExtendStyles.Add(cssFile);
        });

        return context;
    }
}