using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor.Services;

public class SecitsService(IJSRuntime jsRuntime, IOptions<SecitsStylesOptions> options) : ISecitsService
{
    public ValueTask SetCurrentStyle(string style, SecitsThemeInput input)
    {
        var styles = options.Value.Get(input);

        return jsRuntime.InvokeVoidAsync("setCurrentStyle", style, styles);
    }
}