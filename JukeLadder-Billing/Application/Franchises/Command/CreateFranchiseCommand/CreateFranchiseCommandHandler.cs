namespace Application.Franchises.Command.CreateFranchiseCommand;

public class CreateFranchiseCommandHandler : IRequestHandler<CreateFranchiseCommand, string>
{
    private readonly ILogger<CreateFranchiseCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    public CreateFranchiseCommandHandler(ILogger<CreateFranchiseCommandHandler> logger, IApplicationDbContext applicationContext)
    {
        _logger = logger;
        _context = applicationContext;
    }

    async Task<string> IRequestHandler<CreateFranchiseCommand, string>.Handle(CreateFranchiseCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Creating a Franchise for user {idUser}", request.UserId);
            
            var item = new Domain.Entities.Franchise(request.Name, request.UserId, "Tous");
            await _context.Franchise.AddAsync(item, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Franchise created with id", item.Id);
            return item.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during creation of Franchise with error {error}", ex.Message);
            throw;
        }
    }
}

