using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Icons;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class EnableIteratorTreePager<TValue> : IGridColumnRenderer<TValue>, IExtendClassStyleBuilder
    where TValue : class, ITreeItem<TValue>
{
    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    [Parameter]
    public Func<TValue, Task>? Children { get; set; }

    [Parameter]
    public ColumnPosition Position { get; set; }

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    private string? _closeIcon;
    private string? _openIcon;

    public int RealWidth { get; set; } = 100;
    public int Width => 100;
    public int MaxWidth => 50;
    public int MinWidth => 50;
    public bool AutoWidth => false;

    public void BuildExtendClassStyle(ClassStyleContext context)
    {
        context.AppendClass("s-tree");
    }

    protected override void ApplySetting()
    {
        base.ApplySetting();
        _closeIcon ??= IconProvider.GetIcon(IconName.CaretUp);
        _openIcon ??= IconProvider.GetIcon(IconName.CaretDown);
    }

    protected async Task OnToggleTreeItem(TValue item)
    {
        item.IsExpended = !item.IsExpended;

        if (item.Children is null)
        {
            if (Children is not null)
                await Children(item);
        }

        await InvokeAsync(StateHasChanged);
    }
}