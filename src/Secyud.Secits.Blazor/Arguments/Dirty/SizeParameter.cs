using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class SizeParameter : DirtyParameter<IHasSize>
{
    public override bool CheckComponentValid(SPluggableBase c)
    {
        return c is IHasSize;
    }

    protected override void BuildClassStyle(IHasSize c, ClassStyleContext context)
    {
        if (c.Height.IsClass && c.Width.IsClass && c.Height.Value == c.Width.Value)
            context.AppendClass("size-", c.Height.ToString());
        else
        {
            WidthParameter.Append(c, context);
            HeightParameter.Append(c, context);
        }
    }

    protected override bool CheckDirty(IHasSize c, ParameterView view)
    {
        return
            ParameterChanged(nameof(c.Width), c.Width, view) ||
            ParameterChanged(nameof(c.Height), c.Height, view)
            ;
    }
}