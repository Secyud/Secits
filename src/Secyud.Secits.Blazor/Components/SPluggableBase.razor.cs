using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Secyud.Secits.Blazor.Services;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public abstract partial class SPluggableBase : IHasTheme, IHasSize, IHasCustomStyle, IPluggable
{
    private readonly ClassStyleBuilder _classStyleBuilder;
    private readonly List<DirtyParameter> _dirtyParameters = [];
    protected SPluggableContainer PluggableContainer { get; }

    protected SPluggableBase()
    {
        PluggableContainer = new SPluggableContainer(this);
        _classStyleBuilder = new ClassStyleBuilder(BuildClassStyleInner);
    }

    protected virtual string ComponentName => "s";

    [Inject]
    private IDirtyParameterService DirtyParameterService { get; set; } = null!;


    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        _dirtyParameters.AddRange(DirtyParameterService
            .GetDirtyParameters(this));
    }

    #region Parameters

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        if (_dirtyParameters.Any(parameter => parameter.CheckComponentDirty(this, parameters)))
        {
            _classStyleBuilder.SetDirty();
        }

        var container = new ParameterContainer(parameters);
        PreParametersSet(container);
        await base.SetParametersAsync(parameters);
        await container.RunAndCleanTasksAsync();
    }

    protected virtual void PreParametersSet(ParameterContainer parameters)
    {
    }

    public Task SetDirtyAsync()
    {
        _classStyleBuilder.SetDirty();
        return InvokeAsync(StateHasChanged);
    }

    #endregion

    #region Internal

    protected override void OnBuildRenderTree(RenderTreeBuilder builder)
    {
        builder.AddContent(-1, GenerateSettingContent());
        base.OnBuildRenderTree(builder);
    }

    public new void StateHasChanged()
    {
        base.StateHasChanged();
    }

    public new Task InvokeAsync(Action action)
    {
        return base.InvokeAsync(action);
    }

    public new Task InvokeAsync(Func<Task> action)
    {
        return base.InvokeAsync(action);
    }

    #endregion

    #region ClassStyle

    private void BuildClassStyleInner(ClassStyleContext context)
    {
        if (!string.IsNullOrEmpty(ComponentName))
            context.Class.Append("s-" + ComponentName);
        BuildClassStyle(context);
    }

    protected virtual void BuildClassStyle(ClassStyleContext context)
    {
        foreach (var extendClassStyleBuilder in ClassStyleBuilders)
        {
            extendClassStyleBuilder.BuildExtendClassStyle(context);
        }

        foreach (var dirtyParameter in _dirtyParameters)
        {
            dirtyParameter.BuildComponentClassStyle(this, context);
        }
    }

    protected string? GetClass() => _classStyleBuilder.Class;

    protected string? GetStyle() => _classStyleBuilder.Style;

    #endregion

    #region Settings

    public SSettings<IExtendClassStyleBuilder> ClassStyleBuilders { get; } = new();

    #endregion

    #region Parameters

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public Theme Theme { get; set; }

    [Parameter]
    public Size Size { get; set; }

    [Parameter]
    public SValue Width { get; set; }

    [Parameter]
    public SValue Height { get; set; }

    #endregion
}