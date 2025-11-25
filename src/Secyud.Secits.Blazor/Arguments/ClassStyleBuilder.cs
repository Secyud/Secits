namespace Secyud.Secits.Blazor;

/// <summary>
/// Represents a builder for dynamically constructing CSS class and style strings.
/// This class is designed to work in conjunction with a build action that defines
/// how the class and style attributes are constructed. It supports dirty state tracking
/// to indicate when the class or style values need to be rebuilt.
/// The builder leverages a context object to manage the construction process,
/// allowing for flexible and conditional composition of class and style attributes.
/// </summary>
/// <param name="buildAction">A delegate that defines the logic for building the class and style attributes.</param>
public class ClassStyleBuilder(Action<ClassStyleContext> buildAction)
{
    private readonly ClassStyleContext _context = new();
    public bool IsDirty { get; private set; } = true;

    public void SetDirty()
    {
        IsDirty = true;
    }

    private string? _class;

    public string? Class
    {
        get
        {
            TryBuild();
            return _class;
        }
    }

    private string? _style;

    public string? Style
    {
        get
        {
            TryBuild();
            return _style;
        }
    }

    private void TryBuild()
    {
        if (!IsDirty) return;

        _context.Clear();
        buildAction(_context);
        var @class = _context.Class.ToString();
        var style = _context.Style.ToString();
        _class = string.IsNullOrWhiteSpace(@class) ? null : @class;
        _style = string.IsNullOrWhiteSpace(style) ? null : style;
    }

    public static string? GenerateClass(params string?[] strs)
    {
        var res = string.Join(' ', strs.Where(u => !string.IsNullOrEmpty(u)));

        return string.IsNullOrWhiteSpace(res) ? null : res;
    }
}