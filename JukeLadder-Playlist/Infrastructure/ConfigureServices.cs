using Infrastructure.MongoDb;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {        
        services.AddScoped<IMongoDbHelper<Track>, MongoDbHelper<Track>>();
        services.AddScoped<IMongoDbHelper<PlaylistState>, MongoDbHelper<PlaylistState>>();
        services.AddScoped<MongoDbSettings>();
        return services;
    }
}