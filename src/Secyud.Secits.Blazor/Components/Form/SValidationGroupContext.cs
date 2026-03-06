using System.ComponentModel.DataAnnotations;

namespace Secyud.Secits.Blazor;

public class SValidationGroupContext
{
    public List<SValidationContext> FieldContexts { get; } = [];

    public bool IsValid()
    {
        return FieldContexts.All(context => context.IsValid());
    }

    public List<ValidationResult> GetValidationResults()
    {
        List<ValidationResult> results = [];

        foreach (var fieldContext in FieldContexts)
        {
            results.AddRange(fieldContext.GetValidationResults());
        }

        return results;
    }
}