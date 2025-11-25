using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using Secyud.Secits.Blazor.Settings;

namespace Secyud.Secits.Blazor.Validations;

public partial class ValidationForm : IHasContent
{
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    public SSettings<ValidationField> Fields { get; } = new();

    public bool Validate()
    {
        return Fields.All(field => field.ValidationResults.Count <= 0);
    }

    public List<ValidationResult> GetValidationResults()
    {
        List<ValidationResult> results = [];

        foreach (var validationField in Fields)
        {
            results.AddRange(validationField.ValidationResults);
        }

        return results;
    }
}