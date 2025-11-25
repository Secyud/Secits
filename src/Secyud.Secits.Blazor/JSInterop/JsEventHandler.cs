using Microsoft.JSInterop;
using Secyud.Secits.Blazor.Options;

namespace Secyud.Secits.Blazor.JSInterop;

public class JsEventHandler(IJSRuntime js) : JsModule(js, $"./{SecitsStylesOptions.RootPath}js/event-handler.bundle.min.js")
{
}