namespace Secyud.Secits.Blazor.Options;

public class SecitsOptions
{
    public const string RootPath = "_content/Secyud.Secits.Blazor/";
    public List<DirtyParameter> Parameters { get; } = [];

    public List<string> ExtendScripts { get; } = [RootPath + "js/components.bundle.min.js"];
    public List<string> ExtendStyles { get; } = [];
}