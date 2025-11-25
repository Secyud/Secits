using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor;

public static class SecitsFontAwesomeIconsExtensions
{
    public static IServiceCollection AddSecitsFontAwesome(this IServiceCollection services)
    {
        services.AddSingleton<IIconProvider, FontAwesomeIconProvider>();

        services.Configure<SecitsOptions>(options =>
        {
            options.ExtendScripts.Add("_content/Secyud.Secits.Blazor.Icons.FontAwesome/js/all.min.js");
            options.ExtendStyles.Add("_content/Secyud.Secits.Blazor.Icons.FontAwesome/css/all.min.css");
        });

        return services;
    }
}