using Cord.SDK.Application.Abstractions.Messaging;
using Cord.SDK.Objects;

namespace Cord.SDK.Application.Presences.V1.Commands;

public sealed record PresenceUpdateRequest(string Id, PresencePutItem Item) : ICommand<CordResponse>;
