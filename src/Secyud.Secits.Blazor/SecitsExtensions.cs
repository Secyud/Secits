using Microsoft.Extensions.DependencyInjection;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.JSInterop;
using Secyud.Secits.Blazor.Options;
using Secyud.Secits.Blazor.PageRoutes;
using Secyud.Secits.Blazor.Services;
using Secyud.Secits.Blazor.Validations;

namespace Secyud.Secits.Blazor;

public static class SecitsExtensions
{
    public static IServiceCollection AddSecitsBlazor(this IServiceCollection services,
        Action<SecitsOptions>? optionAction = null)
    {
        #region Service

        services.AddSingleton<IDirtyParameterService, DirtyParameterService>();
        services.AddTransient<ISecitsModelValidator, DefaultSecitsModelValidator>();
        services.AddTransient<IIconProvider, NullIconProvider>();
        services.AddTransient<ISecitsService, SecitsService>();
        services.AddScoped<PageRouteManager>();

        #endregion

        #region Js

        services.AddTransient<IJsElement, SJsElement>();
        services.AddTransient<IJsWindow, SJsWindow>();
        services.AddTransient<IJsDocument, SJsDocument>();
        services.AddScoped<JsEventHandler>();

        #endregion

        services.Configure<SecitsOptions>(options =>
        {
            options.Parameters.AddRange([
                new ClassStyleParameter(),
                new HeightParameter(),
                new WidthParameter(),
                new SizeParameter(),
                new ThemeParameter(),
                new ActiveParameter(),
            ]);
            optionAction?.Invoke(options);
        });

        return services;
    }
}