using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor;

public partial class AddRollerBox<TValue>
{
    [Parameter, Range(1, 4)]
    public int NumberCount { get; set; } = 1;

    [Parameter]
    public List<TValue>? Items { get; set; }

    [Parameter]
    public bool Cycle { get; set; }

    protected TValue GetValue()
    {
        if (Master.InputInvoker.Get() is { } invoker)
        {
            return invoker.GetActiveItem();
        }

        return default!;
    }

    protected async Task SetValueAsync(TValue value)
    {
        if (Master.InputInvoker.Get() is { } invoker)
        {
            await invoker.SetActiveItemAsync(this, value);
        }
    }


    public Func<TValue, string?>? TextField
    {
        get
        {
            if (Master.TextField.Get() is { } textField)
            {
                return u => textField.ToString(u);
            }

            return null;
        }
    }
}