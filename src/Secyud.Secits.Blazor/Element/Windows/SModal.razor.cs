using System.ComponentModel.DataAnnotations;

namespace Secyud.Secits.Blazor.Element;

public partial class SModal
{
    private SForm? _validationForm;

    public bool Validate()
    {
        return _validationForm is null || _validationForm.IsValid();
    }

    public List<ValidationResult> GetValidationResults()
    {
        if (_validationForm is null)
            return [];
        List<ValidationResult> results = [];

        foreach (var validationField in _validationForm.Fields)
        {
            results.AddRange(validationField.ValidationResults);
        }

        return results;
    }

    protected override string? GetClass()
    {
        return ClassStyleBuilder.GenerateClass("s-modal", "middle", "center", Class);
    }
}