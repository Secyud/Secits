using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Themes;

namespace Secyud.Secits.Blazor;

/// <summary>
/// container component with styles
/// </summary>
public abstract class CComponentBase : SComponentBase, IContainerComponent, IElementComponent
{
    public ElementReference ElementRef { get; protected set; }

    protected virtual string? ComponentClass => null;

    private readonly Lazy<IReadOnlyList<IDirtyParameter>> _dirtyParameters;
    private readonly ClassStyleBuilder _classStyleBuilder;

    protected CComponentBase()
    {
        _dirtyParameters = new Lazy<IReadOnlyList<IDirtyParameter>>(() =>
            DirtyParameterProvider.GetDirtyParameters(this));
        _classStyleBuilder = new ClassStyleBuilder(ConfigureClassStyleAction);
    }

    [Inject] private IDirtyParameterProvider DirtyParameterProvider { get; set; } = null!;

    [Parameter] public string? Class { get; set; }
    [Parameter] public string? Style { get; set; }

    public void SetDirty()
    {
        _classStyleBuilder.SetDirty();
    }
    
    public void SetDirty<T>(ref T? field, T? value) where T : IEquatable<T>
    {
        _classStyleBuilder.SetDirty(ref field, value);
    }

    /// <summary>
    /// 使用<see cref="IDirtyParameter"/>来判断一个参数是否改变样式，如果改变，则需要重新生成样式。
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public override Task SetParametersAsync(ParameterView parameters)
    {
        _classStyleBuilder.CheckDirtyFromParameterView(
            this, parameters, _dirtyParameters);

        return base.SetParametersAsync(parameters);
    }

    private void ConfigureClassStyleAction(ClassStyleContext context)
    {
        context.AppendClass(ComponentClass);
        ConfigureClassStyle(context);
        this.AddDirtyParameters(context, _dirtyParameters);
    }

    /// <summary>
    /// 部分组件有自己的样式逻辑
    /// </summary>
    /// <param name="context"></param>
    protected virtual void ConfigureClassStyle(ClassStyleContext context)
    {
    }

    protected string? GetClass()
    {
        return _classStyleBuilder.GetClass();
    }

    protected string? GetStyle()
    {
        return _classStyleBuilder.GetStyle();
    }
}