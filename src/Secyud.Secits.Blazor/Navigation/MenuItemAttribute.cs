namespace Secyud.Secits.Blazor.Navigation;

[AttributeUsage(AttributeTargets.Class)]
public class MenuItemAttribute : Attribute
{
    public Type? ResourceType { get; set; }
    public string? DisplayName { get; set; }
}