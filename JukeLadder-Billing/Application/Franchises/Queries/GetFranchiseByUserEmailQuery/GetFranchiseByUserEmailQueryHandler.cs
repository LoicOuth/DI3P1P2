using Application.Franchises.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Application.Franchises.Queries.GetFranchiseByUserEmailQuery;

public class GetFranchiseByUserEmailQueryHandler : IRequestHandler<GetFranchiseByUserEmailQuery, FranchiseDto>
{
    private readonly ILogger<GetFranchiseByUserEmailQueryHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFranchiseByUserEmailQueryHandler(ILogger<GetFranchiseByUserEmailQueryHandler> logger, IApplicationDbContext context, IMapper mapper)
    {
        _logger = logger;
        _context = context;
        _mapper = mapper;
    }

    public Task<FranchiseDto> Handle(GetFranchiseByUserEmailQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Get by id from user email : {query}", request.UserEmail);
            var result = _context.Franchise.ProjectTo<FranchiseDto>(_mapper.ConfigurationProvider).FirstOrDefault(x => x.UserId == request.UserEmail);

            if (result == null)
                throw new NotFoundException();

            _logger.LogInformation("Successfuly get franchise");
            return Task.FromResult(result);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during GetByUserEmail with error : {error}", ex.Message);
            throw;
        }
    }
}
