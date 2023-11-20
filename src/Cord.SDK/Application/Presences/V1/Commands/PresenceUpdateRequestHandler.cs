using Cord.SDK.Application.Abstractions.HttpRequest;
using Cord.SDK.Application.Abstractions.Messaging;
using Cord.SDK.Objects;

namespace Cord.SDK.Application.Presences.V1.Commands;

internal sealed class PresenceUpdateRequestHandler : ICommandHandler<PresenceUpdateRequest, CordResponse>
{
    private readonly IRestClient _restClient;

    public PresenceUpdateRequestHandler(IRestClient restClient)
    {
        _restClient = restClient;
    }

    public async Task<CordResponse> Handle(PresenceUpdateRequest request, CancellationToken cancellationToken)
    {
        return (await _restClient.Put<CordResponse, PresencePutItem>($"/v1/users/{request.Id}/presence", request.Item,
            cancellationToken))!;
    }
}
