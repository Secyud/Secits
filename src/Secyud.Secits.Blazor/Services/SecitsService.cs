using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor.Services;

public class SecitsService(IJSRuntime jsRuntime, IOptions<SecitsStylesOptions> options) : ISecitsService
{
    public ValueTask SetCurrentStyle(string style, SecitsThemeParam param)
    {
        var styles = options.Value.Get(param);

        return jsRuntime.InvokeVoidAsync("setCurrentStyle", style, styles);
    }
}