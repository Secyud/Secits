namespace Secyud.Secits.Blazor.Validations;

public interface IValidationListener
{
    Task OnValidationChangedAsync();
}