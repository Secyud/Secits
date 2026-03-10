using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.JSInterop;
using Secyud.Secits.Blazor.Navigation;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public static class SecitsExtensions
{
    public static IServiceCollection AddSecitsBlazor(this IServiceCollection services,
        Action<SecitsBlazorBuildContext>? buildAction = null)
    {
        #region Service

        services.AddSingleton<IDirtyParameterProvider, DirtyParameterProvider>();
        services.AddTransient<IThemeManager, SThemeManager>();
        services.AddScoped<IAppContext, SAppContext>();
        services.AddScoped<IFormValidator, FormValidator>();
        services.AddScoped<IRouterItemGenerator, RouterItemGenerator>();

        // js
        services.AddTransient<IJsWindow, JsWindow>();

        #endregion

        services.Configure<SecitsOptions>(options =>
        {
            options.Parameters.AddRange([
                new ClassStyleParameter(),
                new VerticalParameter(),
                new HorizontalParameter(),
                new LayoutedParameter(),
                new ThemedParameter(),
                new SizedParameter(),
                new ActivableParameter(),
                new InputParameter(),
            ]);
        });

        var context = new SecitsBlazorBuildContext
        {
            Services = services,
        };

        buildAction?.Invoke(context);

        return services;
    }
}