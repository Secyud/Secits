using Microsoft.AspNetCore.Components.Rendering;

namespace Secyud.Secits.Blazor.Plugins;

public partial class SpInputClear<TValue> : ISpInputElement
{
    public override string PluginName => "input-clear";

    public void GenerateInputElement(RenderTreeBuilder builder)
    {
        BuildRenderTree(builder);
    }

    private void OnIconClick()
    {
        if (Context?.Component is EComponentBase<TValue> c)
        {
            c.TriggerValueChangedEventAsync(default!).ConfigureAwait(false);
        }
    }
}