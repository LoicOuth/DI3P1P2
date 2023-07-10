using Application.Common.Interfaces;
using Application.Deezer.Dto;
using Application.Track.Dto;
using Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Linq;
using System.Threading;

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
        var deezerPlaylistHelper = new Mock<IDeezerPlaylistHelper>();
        var solrHelper = new Mock<ISolrHelper>();

        //Deezer
        deezerPlaylistHelper.Setup(x => x.SearchPlaylists("daftpunk"))
            .ReturnsAsync(new System.Collections.Generic.List<SearchPlaylistDto>()
            {
                new SearchPlaylistDto(1, "Daft Punk", "desc", 10, true, "http://url")
            });

        deezerPlaylistHelper.Setup(x => x.SearchTracksPlaylistsWithIdPlaylist("13","0123456789"))
            .ReturnsAsync(new System.Collections.Generic.List<TrackSolrDto>()
            {
                 new TrackSolrDto("1","0123456789", "Daft Punk", "tets", "test", "Tets", 300, "test")
            });

        //Solr
        solrHelper.Setup(x => x.QueryableTracks(SolrFields.Artist.ToString().ToLower(), "Eminem","Tous", It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new System.Collections.Generic.List<TrackSolrDto>()
            {
                        new TrackSolrDto("1", "0123456789", "Daft Punk", "Test","Test","Tests", 5, "test")
            });

        solrHelper.Setup(x => x.GetSuggestions("emi", It.IsAny<CancellationToken>()))
            .ReturnsAsync(new System.Collections.Generic.List<SuggestionSolrDto>()
            {
                                new SuggestionSolrDto("Eminem",2,"Artiste")
            });


        services.Remove<IDeezerPlaylistHelper>();
        services.Remove<ISolrHelper>();

        services.AddSingleton(deezerPlaylistHelper.Object);
        services.AddSingleton(solrHelper.Object);

        return services;
    }
}
