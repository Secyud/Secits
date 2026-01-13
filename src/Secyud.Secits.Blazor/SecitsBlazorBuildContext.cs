using Microsoft.Extensions.DependencyInjection;

namespace Secyud.Secits.Blazor;

public class SecitsBlazorBuildContext
{
    public required IServiceCollection Services { get; init; }
}