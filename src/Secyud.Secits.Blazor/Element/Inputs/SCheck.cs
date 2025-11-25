using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor.Element;

public class SCheck : SIconBase
{
    [Parameter]
    public bool Checked { get; set; }

    private string? _checkedIcon;

    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        _checkedIcon ??= IconProvider.GetIcon(IconName.Check);
    }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-check", "s-icon", Class);
    }

    protected override string? Icon => Checked ? _checkedIcon : null;
}