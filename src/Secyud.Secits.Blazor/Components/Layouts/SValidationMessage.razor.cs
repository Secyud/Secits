using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Validations;

namespace Secyud.Secits.Blazor;

public partial class SValidationMessage : IValidationListener, IAsyncDisposable
{
    private ValidationField? _validationField;

    [CascadingParameter]
    public ValidationField? ValidationField
    {
        get => _validationField;
        set
        {
            _validationField?.Listeners.Forgo(this);
            _validationField = value;
            _validationField?.Listeners.Apply(this);
        }
    }

    public async Task OnValidationChangedAsync()
    {
        await InvokeAsync(StateHasChanged);
    }

    public async ValueTask DisposeAsync()
    {
        ValidationField = null;
        await ValueTask.CompletedTask;
    }
}