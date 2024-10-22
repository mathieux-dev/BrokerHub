using BrokerHub.Application.Command.Imovel.CreateImovel;

namespace BrokerHub.API.Configuration;

[System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(typeof(CreateImovelHandler).Assembly);
        });

        return services;
    }
}
