using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Element;
using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor.Preset;

public class CloseButton : SIconBase
{
    private string? _iconName;

    [Inject]
    private IIconProvider IconProvider { get; set; } = null!;

    protected override string? Icon => _iconName;

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-icon", "anim", Class);
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        await base.SetParametersAsync(parameters);
        _iconName ??= IconProvider.GetIcon(IconName.Cross);
    }
}