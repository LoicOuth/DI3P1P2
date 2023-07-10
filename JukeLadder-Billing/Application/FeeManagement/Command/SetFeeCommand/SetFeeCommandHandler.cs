using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace Application.FeeManagement.Command.SetFeeCommand;

public class SetFeeCommandHandler : IRequestHandler<SetFeeCommand, string>
{
    private readonly ILogger<SetFeeCommandHandler> _logger;
    private readonly IApplicationDbContext _context;
    private readonly IProductHelper _productHelper;
    private readonly IStripeSettings _stripeSettings;

    public SetFeeCommandHandler(ILogger<SetFeeCommandHandler> logger, IApplicationDbContext context, IProductHelper productHelper, IStripeSettings stripeSettings)
    {
        _logger = logger;
        _context = context;
        _productHelper = productHelper;
        _stripeSettings = stripeSettings;

    }

    public async Task<string> Handle(SetFeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Set minimum {requestMin} and maximum {requestMax}", request.Min, request.Max);

            var promote = await _productHelper.GetProductAsync(_stripeSettings.ProductPromote, "default_price");
            var demote = await _productHelper.GetProductAsync(_stripeSettings.ProductDemote, "default_price");

            if (promote == null || demote == null)
                throw new NotFoundException(nameof(Stripe.Product), "Promote or Demote product not found");
            if (promote.DefaultPrice == null || demote.DefaultPrice == null)
                throw new NotFoundException(nameof(Price), "Promote or Demote price not found");
            if (promote.DefaultPrice.UnitAmount!.Value < request.Min || promote.DefaultPrice.UnitAmount!.Value > request.Max)
                throw new BillingException("Promote price is out of range");
            if (demote.DefaultPrice.UnitAmount!.Value < request.Min || demote.DefaultPrice.UnitAmount!.Value > request.Max)
                throw new BillingException("Demote price is out of range");

            var fee = await _context.FeeParameters.Where(x => x.Active).FirstAsync(cancellationToken);
            if (fee == null)
                throw new NotFoundException(nameof(FeeParameter));

            fee.Active = false;
            fee.UpdatedAt = DateTime.UtcNow;
            _context.FeeParameters.Update(fee);

            var newFee = new FeeParameter(request.Min, request.Max, true, DateTime.UtcNow, DateTime.UtcNow);
            await _context.FeeParameters.AddAsync(newFee, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Sucessfuly set min and max fee");
            return newFee.Id;
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during set of fee born with error {error}", ex.Message);
            throw;
        }
    }
}

