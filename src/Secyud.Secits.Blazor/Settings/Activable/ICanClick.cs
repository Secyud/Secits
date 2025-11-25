using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Settings;

/// <summary>
/// the inheritor can be clicked
/// </summary>
public interface ICanClick
{
    public EventCallback Click { get; set; }
}