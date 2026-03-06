using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Navigation;

public class RouterItem
{
    public Guid Id { get; } = Guid.NewGuid();
    public required string DisplayName { get; set; }
    public required RenderFragment Content { get; set; }
    public required Uri Uri { get; set; }
}