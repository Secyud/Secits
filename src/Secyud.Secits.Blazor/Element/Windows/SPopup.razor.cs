namespace Secyud.Secits.Blazor.Element;

public partial class SPopup
{
    protected bool Visible { get; set; }

    public Task HideAsync()
    {
        return InvokeAsync(() =>
        {
            Visible = false;
            StateHasChanged();
        });
    }

    public Task ShowAsync()
    {
        return InvokeAsync(() =>
        {
            Visible = true;
            StateHasChanged();
        });
    }
}