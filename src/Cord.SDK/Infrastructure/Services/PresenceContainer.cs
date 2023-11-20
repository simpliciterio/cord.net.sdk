using Cord.SDK.Application.Abstractions.Services;

namespace Cord.SDK.Infrastructure.Services;

internal sealed class PresenceContainer : IPresenceContainer
{
    public IPresenceV1 V1 { get; }

    public PresenceContainer(IPresenceV1 v1)
    {
        V1 = v1;
    }
}
