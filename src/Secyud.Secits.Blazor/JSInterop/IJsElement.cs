using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.JSInterop;

public interface IJsElement
{
    public ValueTask<DomRect> GetBoundingClientRect(ElementReference element);
}