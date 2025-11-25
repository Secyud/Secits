using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SList<TValue>
{
    protected override string ComponentName => "list";

    #region Settings

    public SSettings<IListColumnRenderer<TValue>> Columns { get; } = new();

    #endregion
}