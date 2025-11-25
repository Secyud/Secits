using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public abstract class SActivablePluginBase<TComponent> : SLayoutPluginBase<TComponent> where TComponent : SPluggableBase, IHasContentRender
{
    [Parameter]
    public bool Readonly { get; set; }

    [Parameter]
    public bool Disabled { get; set; }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("anim",
            Readonly ? "readonly" : null,
            Disabled ? "disabled" : null,
            Class);
    }
}