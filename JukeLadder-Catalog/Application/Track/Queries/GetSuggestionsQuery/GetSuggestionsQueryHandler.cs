using Application.Common.Interfaces;
using Application.Track.Dto;
using Microsoft.Extensions.Logging;

namespace Application.Track.Queries.GetSuggestionsQuery;

public class GetSuggestionsQueryHandler : IRequestHandler<GetSuggestionsQuery, List<SuggestionSolrDto>>
{
    private readonly ILogger<GetSuggestionsQueryHandler> _logger;
    private readonly ISolrHelper _solrTrackHelper;

    public GetSuggestionsQueryHandler(ILogger<GetSuggestionsQueryHandler> logger, ISolrHelper solrTrackHelper)
    {
        _logger = logger;
        _solrTrackHelper = solrTrackHelper;
    }
    public async Task<List<SuggestionSolrDto>> Handle(GetSuggestionsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Get suggestions from query : {query}", request.Query);
            return await _solrTrackHelper.GetSuggestions(request.Query, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during GetSuggestions with error : {error}", ex.Message);
            throw;
        }

    }
}
