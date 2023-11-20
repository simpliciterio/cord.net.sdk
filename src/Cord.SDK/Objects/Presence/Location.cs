using Newtonsoft.Json;

namespace Cord.SDK.Objects;

public abstract class Location
{
    public Location(string page)
    {
        Page = page;
    }
    [JsonProperty("page")]
    public string Page { get; set; }
}
