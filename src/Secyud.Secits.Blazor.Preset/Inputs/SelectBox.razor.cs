using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Preset;

[CascadingTypeParameter(nameof(TValue))]
public partial class SelectBox<TValue>
{
    [Parameter]
    public IReadOnlyList<TValue> Items { get; set; } = [];

    [Parameter]
    public RenderFragment<TValue>? ListItemTemplate { get; set; }

    [Parameter]
    public bool EnableNullable { get; set; }

    [Parameter]
    public TValue Value { get; set; } = default!;

    [Parameter]
    public EventCallback<TValue> ValueChanged { get; set; }

    protected override void BuildClassStyle(ClassStyleContext context)
    {
        base.BuildClassStyle(context);
        context.AppendClass("flex");
    }

    private EnableDropDown? _enableDropDown;

    protected Task CloseDropDownAsync()
    {
        return _enableDropDown?.CloseDropDownAsync() ?? Task.CompletedTask;
    }

    protected Task OpenDropDownAsync()
    {
        return _enableDropDown?.OpenDropDownAsync() ?? Task.CompletedTask;
    }
}