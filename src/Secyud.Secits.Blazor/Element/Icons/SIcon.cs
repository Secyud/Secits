using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Icons;

namespace Secyud.Secits.Blazor.Element;

public class SIcon : SIconBase
{
    private string? _icon;
    private bool _needRefresh = true;

    [Parameter]
    public object? IconName { get; set; }

    [Inject]
    public IIconProvider IconProvider { get; set; } = null!;

    public override Task SetParametersAsync(ParameterView parameters)
    {
        var iconName = parameters.GetValueOrDefault<object?>(nameof(IconName));
        if (iconName != IconName) _needRefresh = true;

        return base.SetParametersAsync(parameters);
    }

    protected override string? Icon
    {
        get
        {
            if (_needRefresh)
            {
                _icon = IconName switch
                {
                    string str => str,
                    IconName icon => IconProvider.GetIcon(icon),
                    _ => IconName?.ToString()
                };
                _needRefresh = false;
            }

            return _icon;
        }
    }
}