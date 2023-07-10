using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.BillingStates.Commands.UpdateBillingStates;

public class UpdateBillingStatesCommandHandler : IRequestHandler<UpdateBillingStatesCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<UpdateBillingStatesCommandHandler> _logger;
    public UpdateBillingStatesCommandHandler(IApplicationDbContext context, ILogger<UpdateBillingStatesCommandHandler> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<Unit> Handle(UpdateBillingStatesCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Update BillingStates for franchise {0}", request.FranchiseId);

            var state = await _context.BillingStates.FirstOrDefaultAsync(x => x.FranchiseId == request.FranchiseId);
            if (state == null)
            {
                _logger.LogInformation("Add State for franchise: {franchiseId}", request.FranchiseId);

                state = new BillingState
                {
                    Enable = request.Enable,
                    FranchiseId = request.FranchiseId,
                    Id = Guid.NewGuid()
                };
                await _context.BillingStates.AddAsync(state, cancellationToken);
            }
            else
                state.Enable = request.Enable;
            
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Successfuly updated BillingStates for franchise {0}", request.FranchiseId);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during update of BillingStates {0}", ex.Message);
            throw;
        }
    }
}
