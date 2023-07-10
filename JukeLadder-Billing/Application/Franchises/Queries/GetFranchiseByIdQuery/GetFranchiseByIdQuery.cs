using Application.Franchises.Dto;

namespace Application.Franchises.Queries.GetFranchiseByIdQuery;
public record GetFranchiseByIdQuery(string Query) : IRequest<FranchiseDto>;

