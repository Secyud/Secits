using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Element;

public abstract class SElementBase : ComponentBase
{
    protected ElementReference Ref;

    public ElementReference ElementRef => Ref;
}