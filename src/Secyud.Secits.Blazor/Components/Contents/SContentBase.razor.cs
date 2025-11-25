using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SContentBase : IHasContentRender
{
    #region Settings

    public SSettings<IContentRenderer> Content { get; } = new();

    #endregion

    public List<IContentRenderer> GetRenders(RendererPosition position)
    {
        return Content
            .Where(u => u.GetLayoutPosition() == position)
            .ToList();
    }
}