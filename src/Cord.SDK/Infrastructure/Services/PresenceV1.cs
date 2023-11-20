using Cord.SDK.Application.Abstractions.Services;
using Cord.SDK.Application.Presences.V1.Commands;
using Cord.SDK.Objects;
using MediatR;

namespace Cord.SDK.Infrastructure.Services;

internal sealed class PresenceV1 : IPresenceV1
{
    private IMediator _mediator;
    public PresenceV1(IMediator mediator) => _mediator = mediator;

    public async Task<CordResponse> Update(string userId, PresencePutItem item, CancellationToken cancellationToken)
    {
        return await _mediator.Send(new PresenceUpdateRequest(userId, item), cancellationToken);
    }
}
