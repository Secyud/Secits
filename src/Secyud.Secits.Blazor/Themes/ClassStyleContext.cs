using System.Text;

namespace Secyud.Secits.Blazor.Themes;

/// <summary>
/// Provides a context for building and managing CSS class and style strings dynamically.
/// </summary>
public class ClassStyleContext
{
    public StringBuilder ClassBuilder { get; } = new();

    public StringBuilder StyleBuilder { get; } = new();

    public void AppendClass(string? @class, params string[] parameters)
    {
        if (string.IsNullOrWhiteSpace(@class)) return;
        ClassBuilder.Append(' ');
        ClassBuilder.Append(@class);
        foreach (var parameter in parameters)
            ClassBuilder.Append(parameter);
    }

    public void AppendStyle(string? name, string? value, bool important = false)
    {
        if (string.IsNullOrWhiteSpace(value)) return;

        StyleBuilder.Append(name).Append(':').Append(value);
        if (important)
            StyleBuilder.Append(" !important");
        StyleBuilder.Append(';');
    }

    public void AppendClassOrStyle(SValue parameter, string? classPrefix = null, string? styleName = null)
    {
        if (parameter.IsNull) return;

        if (parameter.IsClass)
            AppendClass(classPrefix, parameter.Value);
        else
            AppendStyle(styleName, parameter.Value);
    }
}