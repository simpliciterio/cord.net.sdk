using Cord.SDK.Objects;

namespace Cord.SDK.Application.Abstractions.Services;

public interface IPresenceV1
{
    Task<CordResponse> Update(string userId, PresencePutItem item, CancellationToken cancellationToken);
}
