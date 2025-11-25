namespace Secyud.Secits.Blazor.PageRoutes;

[AttributeUsage(AttributeTargets.Class)]
public class PageRouteAttribute : Attribute
{
    public string? Name { get; set; }
    public Type? ResourceType { get; set; }
    public string[] Parameters { get; set; } = [];
    public string[] ParameterPrefixes { get; set; } = [];
}