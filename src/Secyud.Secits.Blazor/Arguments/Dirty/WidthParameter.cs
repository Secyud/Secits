using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class WidthParameter : DirtyParameter<IHasWidth>
{
    public override bool CheckComponentValid(SPluggableBase c)
    {
        return c is IHasWidth and not IHasSize;
    }

    public static void Append(IHasWidth c, ClassStyleContext context)
    {
        context.AppendClassOrStyle(c.Width, "w-", "width");
    }

    protected override void BuildClassStyle(IHasWidth c, ClassStyleContext context)
    {
        Append(c, context);
    }

    protected override bool CheckDirty(IHasWidth c, ParameterView view)
    {
        return ParameterChanged(nameof(c.Width), c.Width, view);
    }
}