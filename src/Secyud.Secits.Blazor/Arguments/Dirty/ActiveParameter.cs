using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class ActiveParameter : DirtyParameter<ICanActive>
{
    public override bool CheckComponentValid(SPluggableBase c)
    {
        return c is ICanActive;
    }

    protected override void BuildClassStyle(ICanActive c, ClassStyleContext context)
    {
        if (c.Disabled) context.AppendClass("disabled");
        if (c.Readonly) context.AppendClass("readonly");
    }

    protected override bool CheckDirty(ICanActive c, ParameterView view)
    {
        return
            ParameterChanged(nameof(c.Readonly), c.Readonly, view) ||
            ParameterChanged(nameof(c.Disabled), c.Disabled, view)
            ;
    }
}