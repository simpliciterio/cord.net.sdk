using Cord.SDK.Application.Abstractions.Services;

namespace Cord.SDK.Infrastructure.Services;

internal sealed class Cord : ICord
{
    public Cord
    (
        IThreadsContainer threadsContainer,
        ITokenGenerator tokenGenerator,
        IUsersContainer usersContainer,
        IOrganizationsContainer organizations,
        IMessagesContainer messages,
        INotificationsContainer notifications,
        IBatchesContainer batches,
        IPresenceContainer presences
    )
    {
        TokenGenerator = tokenGenerator;
        Threads = threadsContainer;
        Users = usersContainer;
        Organizations = organizations;
        Messages = messages;
        Notifications = notifications;
        Batches = batches;
        Presences = presences;
    }

    public ITokenGenerator TokenGenerator { get; }
    public IThreadsContainer Threads { get; }
    public IUsersContainer Users { get; }
    public IOrganizationsContainer Organizations { get; }
    public IMessagesContainer Messages { get; }
    public INotificationsContainer Notifications { get; }
    public IBatchesContainer Batches { get; }
    public IPresenceContainer Presences { get; }
}