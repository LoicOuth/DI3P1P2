using Application.Common.Interfaces;
using Domain;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Application.IntegrationTests;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection Remove<TService>(this IServiceCollection services)
    {
        var serviceDescriptor = services.FirstOrDefault(d =>
            d.ServiceType == typeof(TService));

        if (serviceDescriptor != null)
        {
            services.Remove(serviceDescriptor);
        }

        return services;
    }

    public static IServiceCollection PrepareTest(this IServiceCollection services)
    {
        var mongoHelper = new Mock<IMongoDbHelper<Track>>();
        var mongoHelperState = new Mock<IMongoDbHelper<PlaylistState>>();
        var track = new Track
        {
            Id = "1",
            Title = "test",
            Artist = "test",
            Album = "test",
            Duration = 1,
            Cover = "test",
            Downvotes = new System.Collections.Generic.List<string>(),
            Upvotes = new System.Collections.Generic.List<string>(),
            FranchiseId = "test"
        };

        mongoHelper.Setup(x => x.InsertAsync(It.IsAny<Track>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(track));
        mongoHelper.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Track, bool>>>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(track));
        mongoHelper.Setup(x => x.UpdateAsync(It.IsAny<Expression<Func<Track, bool>>>(), It.IsAny<Track>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(track));        

        services.Remove<IMongoDbHelper<Track>>()
            .AddSingleton(mongoHelper.Object);
        services.Remove<IMongoDbHelper<PlaylistState>>()
            .AddSingleton(mongoHelperState.Object);
        
        return services;
    }
}
