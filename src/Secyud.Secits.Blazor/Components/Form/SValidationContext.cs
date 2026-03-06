using System.ComponentModel.DataAnnotations;

namespace Secyud.Secits.Blazor;

public class SValidationContext
{
    private List<ValidationResult> _validationResults = [];

    public bool IsValid()
    {
        return _validationResults.Count <= 0;
    }

    public List<ValidationResult> GetValidationResults()
    {
        return _validationResults;
    }

    public event EventHandler? ValidationResultChanged;

    public void SetValidationResults(object? sender, IEnumerable<ValidationResult> validationResults)
    {
        _validationResults = validationResults.ToList();
        ValidationResultChanged?.Invoke(sender, EventArgs.Empty);
    }
}