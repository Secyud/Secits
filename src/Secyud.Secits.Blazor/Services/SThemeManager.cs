using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public class SThemeManager(IJSRuntime jsRuntime, IOptions<SecitsStylesOptions> options) : IThemeManager
{
    public ValueTask SetCurrentStyle(string style, SecitsThemeInput input)
    {
        var styles = options.Value.Get(input);

        return SetCurrentStyle(style, styles);
    }

    public ValueTask SetCurrentStyle(string style, List<SecitsStyleFile> styles)
    {
        return jsRuntime.InvokeVoidAsync(SJsModules.Theme.SetCurrentStyle, style, styles);
    }

    public ValueTask ReplaceStyles(List<SecitsStyleFile> styles)
    {
        return jsRuntime.InvokeVoidAsync(SJsModules.Theme.ReplaceStyles, styles);
    }
}