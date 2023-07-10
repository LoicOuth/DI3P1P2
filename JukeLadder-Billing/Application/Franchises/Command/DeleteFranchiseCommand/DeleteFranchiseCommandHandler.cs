using Microsoft.EntityFrameworkCore;

namespace Application.Franchises.Command.DeleteFranchiseCommand;
public class DeleteFranchiseCommandHandler : IRequestHandler<DeleteFranchiseCommand>
{
    private readonly ILogger<DeleteFranchiseCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    public DeleteFranchiseCommandHandler(ILogger<DeleteFranchiseCommandHandler> logger, IApplicationDbContext applicationContext)
    {
        _logger = logger;
        _context = applicationContext;
    }

    public async Task<Unit> Handle(DeleteFranchiseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Deleting a Franchise with id {id}", request.Id);

            var item = await _context.Franchise.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (item == null)
                throw new NotFoundException("Franchise", request.Id);
            _context.Franchise.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Successfuly deleted Franchise with id {id}", request.Id);
            return Unit.Value;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during creation of Franchise with error {error}", ex.Message);
            throw;
        }
    }
}
