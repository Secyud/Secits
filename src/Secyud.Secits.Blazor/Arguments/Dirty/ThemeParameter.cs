using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class ThemeParameter : DirtyParameter<IHasTheme>
{
    public override bool CheckComponentValid(SPluggableBase c)
    {
        return c is IHasTheme;
    }


    protected override void BuildClassStyle(IHasTheme c, ClassStyleContext context)
    {
        context.AppendClass(c.Theme switch
        {
            Theme.Primary => "primary",
            Theme.Secondary => "secondary",
            Theme.Naive => "naive",
            Theme.Success => "success",
            Theme.Info => "info",
            Theme.Warning => "warning",
            Theme.Danger => "danger",
            _ => null
        });

        context.AppendClass(c.Size switch
        {
            Size.XSmall => "xs",
            Size.Small => "sm",
            Size.Medium => "md",
            Size.Large => "lg",
            Size.XLarge => "xl",
            _ => null
        });
    }

    protected override bool CheckDirty(IHasTheme c, ParameterView view)
    {
        return
            ParameterChanged(nameof(c.Size), c.Size, view) ||
            ParameterChanged(nameof(c.Theme), c.Theme, view)
            ;
    }
}