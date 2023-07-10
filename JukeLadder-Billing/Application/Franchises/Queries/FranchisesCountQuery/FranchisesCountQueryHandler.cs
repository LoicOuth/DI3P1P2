using Microsoft.EntityFrameworkCore;

namespace Application.Franchises.Queries.FranchisesCountQuery
{
    public class FranchisesCountQueryHandler : IRequestHandler<FranchisesCountQuery, int>
    {
        private readonly ILogger<FranchisesCountQueryHandler> _logger;
        private readonly IApplicationDbContext _context;
        public FranchisesCountQueryHandler(ILogger<FranchisesCountQueryHandler> logger, IApplicationDbContext applicationContext)
        {
            _logger = logger;
            _context = applicationContext;
        }

        public async Task<int> Handle(FranchisesCountQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Getting count of Franchises");
                return await _context.Franchise.CountAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error during GetAll Franchise with error {error}", ex.Message);
                throw;
            }
        }
    }
}
