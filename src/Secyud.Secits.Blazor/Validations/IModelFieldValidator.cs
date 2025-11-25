using System.ComponentModel.DataAnnotations;

namespace Secyud.Secits.Blazor.Validations;

public interface ISecitsModelValidator
{
    public Task<List<ValidationResult>> ValidateValueAsync(ValidationContext context, object? value);
}