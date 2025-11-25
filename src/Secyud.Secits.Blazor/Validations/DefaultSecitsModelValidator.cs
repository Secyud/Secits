using System.ComponentModel.DataAnnotations;

namespace Secyud.Secits.Blazor.Validations;

public class DefaultSecitsModelValidator : ISecitsModelValidator
{
    public virtual async Task<List<ValidationResult>> ValidateValueAsync(ValidationContext context, object? value)
    {
        var results = new List<ValidationResult>();

        Validator.TryValidateProperty(value, context, results);

        return await Task.FromResult(results);
    }
}