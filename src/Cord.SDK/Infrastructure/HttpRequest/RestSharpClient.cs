using Cord.SDK.Exceptions;
using Cord.SDK.Objects;
using RestSharp;
using RestSharp.Authenticators;
using IRestClient = Cord.SDK.Application.Abstractions.HttpRequest.IRestClient;
using System.Net;

namespace Cord.SDK.Infrastructure.HttpRequest;

internal sealed class RestSharpClient : IRestClient
{
    private readonly RestClient _restClient;
    private readonly ITokenGenerator _tokenGenerator;
    private static readonly HttpStatusCode[] _errorStatuses = new[] { HttpStatusCode.BadRequest, HttpStatusCode.InternalServerError, HttpStatusCode.Unauthorized, HttpStatusCode.Conflict };

    public RestSharpClient(RestClient restClient, ITokenGenerator tokenGenerator)
    {
        _restClient = restClient;
        _tokenGenerator = tokenGenerator;
    }

    private RestRequest GenerateRequest(string uri, Method method = Method.Get)
    {
        return new RestRequest
        {
            Authenticator = new JwtAuthenticator(_tokenGenerator.GenerateServerToken()),
            Resource = uri,
            Method = method
        };
    }

    public async Task<TResponse?> Get<TResponse>(string uri, CancellationToken cancellationToken = default)
    {
        var request = GenerateRequest(uri);
        var response = await _restClient.ExecuteAsync<TResponse>(request, cancellationToken);
        ValidateResponse(response);
        return response.Data;
    }

    public async Task<TResponse?> Get<TResponse>(string uri,
        IEnumerable<(string key, string value, bool? encode)> queryParams,
        CancellationToken cancellationToken = default)
    {
        var request = GenerateRequest(uri);
        request.Parameters.AddParameters(queryParams.Select(x => new QueryParameter(x.key, x.value, x.encode ?? false))
            .ToList());
        var response = await _restClient.ExecuteAsync<TResponse>(request, cancellationToken);
        ValidateResponse(response);
        return response.Data;
    }

    public async Task<TResponse?> Put<TResponse, TBody>(string uri, TBody body, CancellationToken cancellationToken)
    {
        var request = GenerateRequest(uri, Method.Put);
        request.AddBody(body!);
        var response = await _restClient.ExecuteAsync<TResponse>(request, cancellationToken);
        ValidateResponse(response);
        return response.Data;
    }

    public async Task<TResponse?> Post<TResponse, TBody>(string uri, TBody body, CancellationToken cancellationToken)
    {
        var request = GenerateRequest(uri, Method.Post);
        request.AddBody(body!);
        var response = await _restClient.ExecuteAsync<TResponse>(request, cancellationToken);
        ValidateResponse(response);
        return response.Data;
    }

    public async Task<TResponse?> Delete<TResponse>(string uri, CancellationToken cancellationToken)
    {
        var request = GenerateRequest(uri, Method.Delete);
        var response = await _restClient.ExecuteAsync<TResponse>(request, cancellationToken);
        ValidateResponse(response);
        return response.Data;
    }

    private void ValidateResponse<T>(RestResponse<T> response)
    {
        if (!_errorStatuses.Contains(response.StatusCode)) return;
        CordErrorResponse? errorResponse =
            Newtonsoft.Json.JsonConvert.DeserializeObject<CordErrorResponse>(response?.Content ?? "");
        CordException.ThrowExceptionFromHttpResponse(response!.StatusCode, errorResponse?.Error ?? response?.Content,
            errorResponse?.Message ?? response?.Content);
    }
}