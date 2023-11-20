using Cord.SDK.Application.Abstractions.Services;

namespace Cord.SDK;

public interface ICord
{
    /// <summary>
    /// A token-generating service that users or the system will use to access the Cord.
    /// </summary>
    /// <value></value>
    ITokenGenerator TokenGenerator { get; }
    /// <summary>
    /// Rest service for the Threads.
    /// </summary>
    /// <value></value>
    IThreadsContainer Threads { get; }
    /// <summary>
    /// Rest service for the Users.
    /// </summary>
    /// <value></value>
    IUsersContainer Users { get; }
    /// <summary>
    /// Rest service for the Organizations.
    /// </summary>
    /// <value></value>
    IOrganizationsContainer Organizations { get; }
    /// <summary>
    /// All available operations for listing and editing messages.
    /// </summary>
    /// <value></value>
    IMessagesContainer Messages { get; }
    /// <summary>
    /// All available operations for sending and manipulating notifications
    /// </summary>
    INotificationsContainer Notifications { get; }
    /// <summary>
    /// Using a single API call to update multiple users and organizations
    /// </summary>
    IBatchesContainer Batches { get; }

    /// <summary>
    /// All available operations for user presence
    /// </summary>
    IPresenceContainer Presences {get;}
}