using System.Text;

namespace Secyud.Secits.Blazor;

/// <summary>
/// Provides a context for building and managing CSS class and style strings dynamically.
/// This class is designed to assist in constructing class and style attributes for components
/// by offering methods to append, clear, and conditionally build class and style values.
/// It supports appending individual class or style entries, handling conditional logic for
/// class or style selection, and integrating with parameter-based dirty state management.
/// </summary>
public class ClassStyleContext
{
    public StringBuilder Class { get; } = new();

    public StringBuilder Style { get; } = new();

    public void Clear()
    {
        Class.Clear();
        Style.Clear();
    }

    public void AppendClass(string? @class, params string[] parameters)
    {
        if (string.IsNullOrWhiteSpace(@class)) return;
        Class.Append(' ');
        Class.Append(@class);
        foreach (var parameter in parameters)
            Class.Append(parameter);
    }

    public void AppendStyle(string name, string? value, bool important = false)
    {
        if (string.IsNullOrWhiteSpace(value)) return;

        Style.Append(name);
        Style.Append(':');
        Style.Append(value);
        if (important)
            Style.Append(" !important");
        Style.Append(';');
    }

    public void AppendClassOrStyle(IMaybeClassParameter parameter, string classPrefix, string styleName)
    {
        if (parameter.IsClass)
            AppendClass(classPrefix, parameter.Value);
        else
            AppendStyle(styleName, parameter.Value);
    }
}