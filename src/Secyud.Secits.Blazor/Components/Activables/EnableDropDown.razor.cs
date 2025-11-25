using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Element;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableDropDown : IHasContent
{
    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public string? ContentClass { get; set; }

    [Parameter]
    public string? ContentStyle { get; set; }

    private SIcon? _icon;
    private string? _upIcon;
    private string? _downIcon;
    private bool _contextVisible;

    private bool ContextVisible
    {
        get => _contextVisible;
        set
        {
            _contextVisible = value;
            StateHasChanged();
        }
    }

    protected override void ApplySetting()
    {
        base.ApplySetting();
        _upIcon ??= IconProvider.GetIcon(IconName.CaretUp);
        _downIcon ??= IconProvider.GetIcon(IconName.CaretDown);
    }

    protected string? GetContentStyle()
    {
        return ContentStyle;
    }

    protected string? GetContentClass()
    {
        return ClassStyleBuilder.GenerateClass("s-dropdown-content", ContentClass);
    }

    public Task CloseDropDownAsync()
    {
        ContextVisible = false;
        return Task.CompletedTask;
    }

    public Task OpenDropDownAsync()
    {
        ContextVisible = true;
        return Task.CompletedTask;
    }
}