using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

/// <summary>
/// container component with styles
/// </summary>
public abstract class CComponentBase : SComponentBase, IContainerComponent
{
    [Inject] private IDirtyParameterProvider DirtyParameterProvider { get; set; } = null!;

    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Style { get; set; }

    public ElementReference ElementRef { get; protected set; }

    protected virtual string? ComponentClass => null;

    private IReadOnlyList<IDirtyParameter>? _dirtyParameters;
    private string? _builtClass;
    private string? _builtStyle;
    private bool _isDirty = true;

    protected override void OnInitialized()
    {
        _dirtyParameters = DirtyParameterProvider.GetDirtyParameters(this);
    }

    public void SetDirty()
    {
        _isDirty = true;
    }


    /// <summary>
    /// 使用<see cref="IDirtyParameter"/>来判断一个参数是否改变样式，如果改变，则需要重新生成样式。
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        if (!_isDirty && _dirtyParameters is not null)
        {
            foreach (var dirtyParameter in _dirtyParameters)
            {
                if (dirtyParameter.CheckComponentDirty(this, parameters))
                {
                    SetDirty();
                    break;
                }
            }
        }

        return base.SetParametersAsync(parameters);
    }

    /// <summary>
    /// 生成样式和类
    /// </summary>
    private void GenerateClassAndStyle()
    {
        if (!_isDirty) return;
        var context = new ClassStyleContext();

        ConfigureClassStyle(context);

        if (_dirtyParameters is not null)
        {
            foreach (var dirtyParameter in _dirtyParameters)
            {
                dirtyParameter.BuildComponentClassStyle(this, context);
            }
        }
        else
        {
            return;
        }

        var cls = context.ClassBuilder.ToString();
        _builtClass = string.IsNullOrWhiteSpace(cls) ? null : cls;
        var stl = context.StyleBuilder.ToString();
        _builtStyle = string.IsNullOrWhiteSpace(stl) ? null : stl;
        _isDirty = false;
    }

    /// <summary>
    /// 部分组件有自己的样式逻辑
    /// </summary>
    /// <param name="context"></param>
    protected virtual void ConfigureClassStyle(ClassStyleContext context)
    {
        context.AppendClass(ComponentClass);
    }

    protected string? GetClass()
    {
        GenerateClassAndStyle();
        return _builtClass;
    }

    protected string? GetStyle()
    {
        GenerateClassAndStyle();
        return _builtStyle;
    }
}