using Application.PaymentIntent.Command.Dto;

namespace Application.PaymentIntent.Command.CreatePaymentIntentWithProductIdCommand;

public class CreatePaymentIntentWithProductIdCommandHandler : IRequestHandler<CreatePaymentIntentWithProductIdCommand, PaymentIntentDto>
{
    private readonly IProductHelper _productHelper;
    private readonly IPaymentIntentHelper _paymentIntentHelper;
    private readonly ILogger<CreatePaymentIntentWithProductIdCommandHandler> _logger;
    public CreatePaymentIntentWithProductIdCommandHandler(ILogger<CreatePaymentIntentWithProductIdCommandHandler> logger, IPaymentIntentHelper paymentIntentHelper, IProductHelper productHelper)
    {
        _logger = logger;
        _paymentIntentHelper = paymentIntentHelper;
        _productHelper = productHelper;
    }
    public async Task<PaymentIntentDto> Handle(CreatePaymentIntentWithProductIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = await _productHelper.GetProductAsync(request.ProductId, "default_price");
            if (product == null)
                throw new NotFoundException(nameof(product), request.ProductId);
            if (product.DefaultPrice == null)
                throw new NotFoundException(nameof(Price), $"Pprice not found for product {request.ProductId}");

            _logger.LogInformation("Creating a PaymentIntend for product {productId} of {amount} {currency}",
                product.Id, product.DefaultPrice.UnitAmount, product.DefaultPrice.Currency);
            
            var paymentIntent = await _paymentIntentHelper.CreateAsync(product.DefaultPrice.UnitAmount!.Value, product.DefaultPrice.Currency, product.Description, request.FranchiseId, request.Title, request.Artist, request.Album, request.Cover, request.Duration, request.DeezerId, cancellationToken);

            _logger.LogInformation("PaymentIntend created with id {id}", paymentIntent.Id);
            _logger.LogTrace("PaymentIntend created with secret {secret}", paymentIntent.ClientSecret);
            return new PaymentIntentDto(paymentIntent.ClientSecret);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during creation of PaymentIntend with error {error}", ex.Message);
            throw;
        }
    }
}
