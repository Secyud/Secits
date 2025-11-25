using System.Text.Json.Serialization;

namespace Secyud.Secits.Blazor;

public class SecitsStyleFile(string path, string id)
{
    [JsonPropertyName("path")]
    public string Path { get; set; } = path;

    [JsonPropertyName("id")]
    public string Id { get; set; } = id;
}