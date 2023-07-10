using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.BillingStates.Queries.GetBillingStateQuery;

public class GetBillingStateQueryHandler : IRequestHandler<GetBillingStateQuery, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<GetBillingStateQueryHandler> _logger;
    public GetBillingStateQueryHandler(IApplicationDbContext context, ILogger<GetBillingStateQueryHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    public async Task<bool> Handle(GetBillingStateQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Get Billing State for Franchise {franchiseId}", request.FranchiseId);
            var state = await _context.BillingStates.FirstOrDefaultAsync(x => x.FranchiseId == request.FranchiseId);

            if (state == null)
            {
                _logger.LogInformation("Add State for franchise: {franchiseId}", request.FranchiseId);

                state = new BillingState
                {
                    Enable = true,
                    FranchiseId = request.FranchiseId,
                    Id = Guid.NewGuid()
                };
                await _context.BillingStates.AddAsync(state, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
            }

            return state.Enable;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during GetBillingState {0}", ex.Message);
            throw;
        }
    }
}
