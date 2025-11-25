using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public class ClassStyleParameter : DirtyParameter<SPluggableBase>
{
    public override bool CheckComponentValid(SPluggableBase c)
    {
        return true;
    }

    protected override void BuildClassStyle(SPluggableBase c, ClassStyleContext context)
    {
        context.AppendClass(c.Class);
        context.Style.Append(c.Style);
    }

    protected override bool CheckDirty(SPluggableBase c, ParameterView view)
    {
        return
            ParameterChanged(nameof(c.Class), c.Class, view) ||
            ParameterChanged(nameof(c.Style), c.Style, view)
            ;
    }
}