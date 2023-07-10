using Application.Deezer.Dto;

namespace Application.Deezer.Queries.SearchPlaylistQuery;

public record SearchPlaylistQuery(string Query) : IRequest<List<SearchPlaylistDto>>;