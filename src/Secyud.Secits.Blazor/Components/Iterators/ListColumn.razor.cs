using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class ListColumn<TValue> : IListColumnRenderer<TValue>
{
    [Parameter]
    public RenderFragment<TValue>? ChildContent { get; set; }

    protected override void ApplySetting()
    {
        Master.Columns.Apply(this);
    }

    protected override void ForgoSetting()
    {
        Master.Columns.Forgo(this);
    }
}