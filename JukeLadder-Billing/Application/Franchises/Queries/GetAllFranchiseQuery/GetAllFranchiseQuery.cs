using Application.Franchises.Dto;

namespace Application.Franchises.Queries.GetAllFranchiseQuery;

public record GetAllFranchiseQuery() : IRequest<List<FranchiseDto>>;