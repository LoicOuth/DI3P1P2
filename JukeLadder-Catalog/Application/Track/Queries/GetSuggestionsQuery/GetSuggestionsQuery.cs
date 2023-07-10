using Application.Track.Dto;

namespace Application.Track.Queries.GetSuggestionsQuery;

public record GetSuggestionsQuery(string Query) : IRequest<List<SuggestionSolrDto>>;
