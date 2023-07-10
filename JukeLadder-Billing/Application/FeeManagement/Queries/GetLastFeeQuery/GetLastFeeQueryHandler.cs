using Application.Common.Mappings;
using Application.FeeManagement.Dto;
using AutoMapper;

namespace Application.FeeManagement.Queries.GetLastFeeQuery
{
    public class GetLastFeeQueryHandler : IRequestHandler<GetLastFeeQuery, FeeDto>
    {
        private readonly ILogger<GetLastFeeQueryHandler> _logger;
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetLastFeeQueryHandler(ILogger<GetLastFeeQueryHandler> logger, IApplicationDbContext applicationContext, IMapper mapper)
        {
            _logger = logger;
            _context = applicationContext;
            _mapper = mapper;
        }

        public async Task<FeeDto> Handle(GetLastFeeQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Get Fee");
                var fee = await _context.FeeParameters.Where(x => x.Active).ProjectToListAsync<FeeDto>(_mapper.ConfigurationProvider);
                if (!fee.Any())
                    throw new NotFoundException("No fee found");
                return fee.Last();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during get last fee with error {error}", ex.Message);
                throw;
            }
        }
    }
}
