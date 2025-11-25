using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Preset;

[CascadingTypeParameter(nameof(TValue))]
[CascadingTypeParameter(nameof(TField))]
public partial class ComboBox<TValue, TField>
{
    [Parameter]
    public IReadOnlyList<TValue> Items { get; set; } = [];

    [Parameter]
    public Func<TValue, TField>? ValueField { get; set; }

    [Parameter]
    public Func<TField, Task<TValue>>? ItemFinder { get; set; }

    [Parameter]
    public RenderFragment<TValue>? ListItemTemplate { get; set; }

    [Parameter]
    public bool EnableNullable { get; set; }

    [Parameter]
    public TField Field { get; set; } = default!;

    [Parameter]
    public EventCallback<TField> FieldChanged { get; set; }

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