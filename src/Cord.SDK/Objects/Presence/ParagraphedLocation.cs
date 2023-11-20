using Newtonsoft.Json;

namespace Cord.SDK.Objects;

public sealed class ParagraphedLocation : Location
{
    public ParagraphedLocation(string page) : base(page)
    {
    }
    [JsonProperty("paragraph")]
    public string? Paragraph { get; set; }
}