namespace Secyud.Secits.Blazor.Element;

public class OverlayContext(OverlayTrigger trigger)
{
    public OverlayTrigger Trigger { get; } = trigger;
    public OverlayTrigger? ParentTrigger { get; set; }
}