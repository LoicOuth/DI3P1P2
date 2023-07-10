using Microsoft.EntityFrameworkCore;

namespace Application.Franchises.Command.UpdateFranchiseCommand;
public class UpdateFranchiseCommandHandler : IRequestHandler<UpdateFranchiseCommand>
{
    private readonly ILogger<UpdateFranchiseCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    public UpdateFranchiseCommandHandler(ILogger<UpdateFranchiseCommandHandler> logger, IApplicationDbContext applicationContext)
    {
        _logger = logger;
        _context = applicationContext;
    }

    public async Task<Unit> Handle(UpdateFranchiseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Updating a Franchise with id {id}", request.FranchiseId);

            var item = await _context.Franchise.FirstOrDefaultAsync(x => x.Id == request.FranchiseId, cancellationToken);
            if (item == null)
                throw new NotFoundException("Franchise", request.FranchiseId);

            item.Name = request.Name;
            item.UserId = request.UserId;
            item.Theme = request.Theme;
            _context.Franchise.Update(item);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Franchise with {id} updated", request.FranchiseId);

            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during update of Franchise with error {error}", ex.Message);
            throw;
        }
    }
}

