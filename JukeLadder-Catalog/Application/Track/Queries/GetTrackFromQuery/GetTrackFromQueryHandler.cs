using Application.Common.Interfaces;
using Application.Track.Dto;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Application.Track.Queries.GetTrackFromQuery;

public class GetTrackFromQueryHandler : IRequestHandler<GetTrackFromQuery, List<TrackSolrDto>>
{
    private readonly ILogger<GetTrackFromQueryHandler> _logger;
    private readonly ISolrHelper _solrTrackHelper;

    public GetTrackFromQueryHandler(ILogger<GetTrackFromQueryHandler> logger, ISolrHelper solrTrackHelper)
    {
        _logger = logger;
        _solrTrackHelper = solrTrackHelper;
    }

    public async Task<List<TrackSolrDto>> Handle(GetTrackFromQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Get Track solr from field : {field} with value {value}, theme {genre}, franchiseId {franchiseId}", request.Field, request.Value, request.Genre, request.FranchiseId);

            var result = await _solrTrackHelper.QueryableTracks(request.Field.ToString().ToLower(), request.Value, request.Genre, request.FranchiseId, cancellationToken);

            return result.ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during GetTrackFrom with error : {error}", ex.Message);

            throw;
        }

    }
}
