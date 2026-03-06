using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor.Themes;

public class InputParameter : IDirtyParameter
{
    public bool CheckComponentValid(IComponent c)
    {
        return c is IInputComponent;
    }

    public bool CheckComponentDirty(IComponent c, ParameterView view)
    {
        if (c is IInputComponent i)
        {
            return i.Readonly != view.GetValueOrDefault<bool>(nameof(i.Readonly));
        }

        return false;
    }

    public void BuildComponentClassStyle(IComponent c, ClassStyleContext context)
    {
        if (c is IInputComponent { Readonly: true })
            context.AppendClass("readonly");
    }
}