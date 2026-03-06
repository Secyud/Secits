using System.ComponentModel.DataAnnotations;

namespace Secyud.Secits.Blazor;

public interface IFormValidator
{
    public Task<List<ValidationResult>> ValidateValueAsync(ValidationContext context, object? value);
}