using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

public class SecitsOptions
{
    public const string RootPath = "_content/Secyud.Secits.Blazor/";
    public List<IDirtyParameter> Parameters { get; } = [];
    public List<string> ExtendScripts { get; } = [RootPath + "js/components.bundle.min.js"];
    public List<string> ExtendStyles { get; } = [RootPath + "css/secits-icons.min.css"];
}