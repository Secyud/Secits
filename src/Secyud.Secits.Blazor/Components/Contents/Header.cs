using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class Header : AddContentBase
{
    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("header", Class);
    }
}