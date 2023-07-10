using Application.FeeManagement.Dto;

namespace Application.FeeManagement.Queries.GetLastFeeQuery;

public record GetLastFeeQuery() : IRequest<FeeDto>;
