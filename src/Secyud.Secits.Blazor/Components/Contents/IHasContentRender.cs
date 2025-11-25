using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public interface IHasContentRender
{
    SSettings<IContentRenderer> Content { get; }
}