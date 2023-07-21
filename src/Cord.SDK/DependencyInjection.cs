using Cord.SDK.Infrastructure.HttpRequest;
using Cord.SDK.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using IRestClient = Cord.SDK.Application.Abstractions.HttpRequest.IRestClient;

namespace Cord.SDK;

public static class DependencyInjection
{
    public static IServiceCollection AddCordSdk(this IServiceCollection serviceCollection,
        Action<CordSdkOptions> configure)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        serviceCollection.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        return serviceCollection
            .ConfigureOptions(configure)
            .ConfigureRestClient()
            .ConfigureSubServices();
    }

    private static IServiceCollection ConfigureOptions(this IServiceCollection serviceCollection,
        Action<CordSdkOptions> configure)
    {
        var options = new CordSdkOptions();
        configure.Invoke(options);
        ArgumentNullException.ThrowIfNull(options.ApplicationId, nameof(options.ApplicationId));
        ArgumentNullException.ThrowIfNull(options.Secret, nameof(options.Secret));
        serviceCollection.AddSingleton(options);
        return serviceCollection;
    }

    private static IServiceCollection ConfigureRestClient(this IServiceCollection serviceCollection)
    {
        var restOptions = new RestClientOptions("https://api.cord.com");
        var restClient = new RestClient(restOptions, configureSerialization: s => s.UseNewtonsoftJson());
        serviceCollection.AddSingleton(restClient);
        serviceCollection.AddScoped<IRestClient, RestSharpClient>();
        return serviceCollection;
    }

    private static IServiceCollection ConfigureSubServices(this IServiceCollection serviceCollection)
    {
        return serviceCollection
            .AddScoped<ICord, Infrastructure.Services.Cord>()
            .AddSingleton<ITokenGenerator, CordTokenGenerator>()
            .AddScoped<IThreadsV1, ThreadsV1>()
            .AddScoped<IThreadsContainer, ThreadsContainer>()
            .AddScoped<IUsersV1, UsersV1>()
            .AddScoped<IUsersContainer, UsersContainer>()
            .AddScoped<IOrganizationsV1, OrganizationsV1>()
            .AddScoped<IOrganizationsContainer, OrganizationsContainer>();
    }
}