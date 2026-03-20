using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

public class ClassStyleBuilder(Action<ClassStyleContext> configureAction)
{
    private string? _builtClass;
    private string? _builtStyle;
    private bool _isDirty = true;

    public void SetDirty()
    {
        _isDirty = true;
    }

    public void CheckDirtyFromParameterView(IComponent component,
        ParameterView parameters, Lazy<IReadOnlyList<IDirtyParameter>> dirtyParameters)
    {
        if (_isDirty) return;
        foreach (var dirtyParameter in dirtyParameters.Value)
        {
            if (dirtyParameter.CheckComponentDirty(component, parameters))
            {
                SetDirty();
                break;
            }
        }
    }
    
    /// <summary>
    /// 生成样式和类
    /// </summary>
    private void GenerateClassAndStyle()
    {
        if (!_isDirty) return;
        var context = new ClassStyleContext();

        configureAction(context);

        var cls = context.ClassBuilder.ToString();
        _builtClass = string.IsNullOrWhiteSpace(cls) ? null : cls;
        var stl = context.StyleBuilder.ToString();
        _builtStyle = string.IsNullOrWhiteSpace(stl) ? null : stl;
        _isDirty = false;
    }

    public string? GetClass()
    {
        GenerateClassAndStyle();
        return _builtClass;
    }

    public string? GetStyle()
    {
        GenerateClassAndStyle();
        return _builtStyle;
    }
}