using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class HeightParameter : DirtyParameter<IHasHeight>
{
    public override bool CheckComponentValid(SPluggableBase c)
    {
        return c is IHasHeight and not IHasSize;
    }

    public static void Append(IHasHeight c, ClassStyleContext context)
    {
        context.AppendClassOrStyle(c.Height, "h-", "height");
    }

    protected override void BuildClassStyle(IHasHeight c, ClassStyleContext context)
    {
        Append(c, context);
    }

    protected override bool CheckDirty(IHasHeight c, ParameterView view)
    {
        return ParameterChanged(nameof(c.Height), c.Height, view);
    }
}