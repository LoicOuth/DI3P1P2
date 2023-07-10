using Application.Franchises.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Franchises.Queries.GetAllFranchiseQuery
{
    public class GetAllFranchiseQueryHandler : IRequestHandler<GetAllFranchiseQuery, List<FranchiseDto>>
    {
        private readonly ILogger<GetAllFranchiseQueryHandler> _logger;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public GetAllFranchiseQueryHandler(ILogger<GetAllFranchiseQueryHandler> logger, IApplicationDbContext applicationContext, IMapper mapper)
        {
            _logger = logger;
            _context = applicationContext;
            _mapper = mapper;
        }

        async Task<List<FranchiseDto>> IRequestHandler<GetAllFranchiseQuery, List<FranchiseDto>>.Handle(GetAllFranchiseQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Get All Franchise");
                return await _context.Franchise.ProjectTo<FranchiseDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during GetAll Franchise with error {error}", ex.Message);
                throw;
            }
        }
    }
}
