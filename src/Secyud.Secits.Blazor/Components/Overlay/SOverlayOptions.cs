using System.Text.Json.Serialization;

namespace Secyud.Secits.Blazor;

public class SOverlayOptions
{
    [JsonPropertyName("controlType")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required SOverlayControlType ControlType { get; set; }

    [JsonPropertyName("ith")]
    public required int HorizontalInterval { get; set; }

    [JsonPropertyName("itv")] public required int VerticalInterval { get; set; }
}