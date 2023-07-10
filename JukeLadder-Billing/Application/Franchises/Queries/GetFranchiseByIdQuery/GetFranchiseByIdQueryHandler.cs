using Application.Franchises.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Franchises.Queries.GetFranchiseByIdQuery;

public class GetFranchiseByIdQueryHandler : IRequestHandler<GetFranchiseByIdQuery, FranchiseDto>
{
    private readonly ILogger<GetFranchiseByIdQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFranchiseByIdQueryHandler(ILogger<GetFranchiseByIdQueryHandler> logger, IApplicationDbContext context, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }
    public Task<FranchiseDto> Handle(GetFranchiseByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Get Franchise with id {id}", request.Query);
            var result = _context.Franchise.ProjectTo<FranchiseDto>(_mapper.ConfigurationProvider).FirstOrDefault(x=>x.Id == request.Query);

            if (result == null)
                throw new NotFoundException();

            _logger.LogInformation("Successfuly get franchise");
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during GetById with error : {error}", ex.Message);
            throw;
        }
    }
}
    