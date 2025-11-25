using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract class SLayoutPluginBase<TComponent> : SPluginBase<TComponent>, IContentRenderer
    where TComponent : SPluggableBase, IHasContentRender
{
    [Parameter]
    public RendererPosition Position { get; set; } = RendererPosition.Body;

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? Class { get; set; }

    public RendererPosition GetLayoutPosition() => Position;

    public RenderFragment RenderTemplate() => BuildRenderTree;

    protected virtual string? GetStyle()
    {
        return Style;
    }

    protected virtual string? GetClass()
    {
        return Class;
    }

    protected override void ApplySetting()
    {
        Master.Content.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.Content.Forgo(this);
    }
}