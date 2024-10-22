using BrokerHub.Application.Command.Imovel.CreateImovel;
using System.Reflection;

namespace BrokerHub.API.Configuration;

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
