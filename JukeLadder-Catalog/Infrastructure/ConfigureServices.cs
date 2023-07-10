using Application.Common.Constants;
using Application.Track.Dto;
using Infrastructure.Deezer;
using Infrastructure.Persistance;
using Infrastructure.Solr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SolrNet;
using System.Data;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlServer(Environment.GetEnvironmentVariable(EnvConst.DB_CONNECTION_STRING) ?? throw new InvalidConstraintException($"ENV {EnvConst.DB_CONNECTION_STRING} NOT SET"),
                        builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddTransient<IDeezerSettings, DeezerSettings>();
            services.AddTransient<IDeezerPlaylistHelper, DeezerPlaylistHelper>();

            services.AddTransient<ISolrHelper, SolrHelper>();

            services.AddTransient<SolrSettings>();
            var settings = services.BuildServiceProvider().GetRequiredService<SolrSettings>();

            services.AddSolrNet<TrackSolrDto>(settings.Url, options =>
            {
                options.HttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", settings.GetCredentialBase64());
            });

            return services;
        }
    }

}
