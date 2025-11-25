using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

[CascadingTypeParameter(nameof(TValue))]
public partial class SInput<TValue> : ISelectable
{
    protected override string ComponentName => "input";
    protected SSelectableContainer SelectableContainer { get; }

    [Parameter]
    public string? Name { get; set; }

    public SInput()
    {
        SelectableContainer = new SSelectableContainer(this);
    }


    #region Settings

    public SSettings<IValueContainer> ValueContainer { get; } = new();

    public SSetting<IValueParser<TValue>> ValueConverter { get; } = new();

    public SSetting<IValueTextField<TValue>> TextField { get; } = new();

    public SSetting<IInputInvoker<TValue>> InputInvoker { get; } = new();

    #endregion
}