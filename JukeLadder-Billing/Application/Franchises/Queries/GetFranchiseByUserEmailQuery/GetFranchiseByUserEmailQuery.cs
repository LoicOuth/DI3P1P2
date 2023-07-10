using Application.Franchises.Dto;

namespace Application.Franchises.Queries.GetFranchiseByUserEmailQuery;

public record GetFranchiseByUserEmailQuery(string UserEmail) : IRequest<FranchiseDto>;
