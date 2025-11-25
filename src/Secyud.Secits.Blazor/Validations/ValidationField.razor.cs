using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Validations;

public partial class ValidationField : IHasContent, IAsyncDisposable
{
    private ValidationForm? _validationForm;

    [Inject]
    private ISecitsModelValidator ModelValidator { get; set; } = null!;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [CascadingParameter]
    public ValidationForm? ValidationForm
    {
        get => _validationForm;
        set
        {
            _validationForm?.Fields.Forgo(this);
            _validationForm = value;
            _validationForm?.Fields.Apply(this);
        }
    }

    public SSettings<IValidationListener> Listeners { get; } = new();

    public List<ValidationResult> ValidationResults { get; set; } = [];

    public async Task OnValidationChangedAsync(ValidationContext context, object? value)
    {
        ValidationResults = await ModelValidator.ValidateValueAsync(context, value);
        await Listeners.InvokeAsync(u => u.OnValidationChangedAsync());
    }

    public async ValueTask DisposeAsync()
    {
        ValidationForm = null;
        await ValueTask.CompletedTask;
    }
}