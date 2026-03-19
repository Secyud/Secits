using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public interface IElementComponent
{
    public ElementReference ElementRef { get; }
}