using Newtonsoft.Json;

namespace Cord.SDK.Objects;

/// <summary>
/// <see href="https://docs.cord.com/rest-apis/presence#Update-a-user-s-presence-2"> More Details</see> 
/// </summary>
public sealed class PresencePutItem
{
    /// <summary>
    /// Creates a "PresencePutItem" instance with required parameters.
    /// </summary>
    /// <param name="location">Required</param>
    /// <param name="groupId">Required</param>
    public PresencePutItem(Location location, string groupId)
    {
        Location = location;
        GroupId = groupId;
    }

    /// <summary>
    /// Creates a "PresencePutItem" instance with required parameters.
    /// </summary>
    /// <param name="location">Required</param>
    /// <param name="groupId">Required</param>
    public PresencePutItem(ParagraphedLocation location, string groupId)
    {
        Location = location;
        GroupId = groupId;
    }

    /// <summary>
    /// Sets an "exclusivity region" for the ephemeral presence set by this update. 
    /// A user can only be present at one location for a given value of exclusiveWithin. 
    /// If the user becomes present at a different location with the same value of exclusiveWithin, they automatically become no longer present at all other locations with that value of exclusive_within. 
    /// This is useful to more easily track presence as a user moves among sub-locations. For example, suppose we'd like to track which specific paragraph on a page a user is present. 
    /// <see href="https://docs.cord.com/rest-apis/presence#exclusiveWithin-2">More details</see>
    /// </summary>
    [JsonProperty("exclusiveWithin")]
    public Location? ExclusiveWithin { get; set; }

    /// <summary>
    /// The location you want the user to be in.
    /// </summary>
    /// <remarks>
    /// Required
    /// </remarks>
    [JsonProperty("location")]
    public Location Location { get; set; }
    /// <summary>
    /// The ID of the group which should be able to see this presence update
    /// </summary>
    /// <remarks>
    /// Required
    /// </remarks>
    [JsonProperty("groupID")]
    public string GroupId { get; set; }
    /// <summary>
    /// When true, this is a durable presence update, when false, or is not used, it is an ephemeral presence update.
    /// </summary>
    /// <remarks>
    /// This value defaults to "false".
    /// </remarks>
    [JsonProperty("durable")]
    public bool? Durable { get; set; }
    /// <summary>
    /// When true, this is an absence update, meaning that the user has just left this location. If the user is currently present at that location, it is cleared. This cannot be used with a durable presence update.
    /// </summary>
    /// <remarks>
    /// This value defaults to false. The user will be set as present at the location.
    /// </remarks>
    [JsonProperty("absent")]
    public bool? Absent { get; set; }
}