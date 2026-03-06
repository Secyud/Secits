using Microsoft.AspNetCore.Components;

namespace Secyud.Secits.Blazor;

public partial class SValidationMessage : IDisposable
{
    [CascadingParameter]
    public SValidationContext? Context
    {
        get;
        set
        {
            if (field == value) return;
            field?.ValidationResultChanged += OnValidationResultChanged;
            field = value;
            field?.ValidationResultChanged -= OnValidationResultChanged;
        }
    }

    public void OnValidationResultChanged(object? sender, EventArgs args)
    {
        InvokeAsync(StateHasChanged).ConfigureAwait(false);
    }

    void IDisposable.Dispose()
    {
        Context = null;
    }
}