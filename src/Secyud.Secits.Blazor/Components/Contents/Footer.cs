using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public class Footer : AddContentBase
{
    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("footer", Class);
    }
}