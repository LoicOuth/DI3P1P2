using Application.Product.Dto;

namespace Application.Product.Command.UpdateProductPriceByIdCommand;

public class UpdateProductPriceByIdCommandHandler : IRequestHandler<UpdateProductPriceByIdCommand, ProductPriceDto>
{
    private readonly IProductHelper _productHelper;
    private readonly ILogger<UpdateProductPriceByIdCommandHandler> _logger;
    public UpdateProductPriceByIdCommandHandler(ILogger<UpdateProductPriceByIdCommandHandler> logger, IProductHelper productHelper)
    {
        _logger = logger;
        _productHelper = productHelper;
    }
    public async Task<ProductPriceDto> Handle(UpdateProductPriceByIdCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Update product {id}, with new price {price}", request.ProductId, request.NewPrice);
            var product = await _productHelper.GetProductAsync(request.ProductId, "default_price");
            if (product == null)
                throw new NotFoundException(nameof(Stripe.Product), request.ProductId);
            if (product.DefaultPrice == null)
                throw new NotFoundException(nameof(Price), $"Pprice not found for product {request.ProductId}");

            var price = await _productHelper.UpdateProductPrice(product.Id, request.NewPrice, request.NewCurrency);
            _logger.LogInformation("Successfuly update product {id}", request.ProductId);
            return new ProductPriceDto(product.Id, price.DefaultPrice.UnitAmount!.Value, price.DefaultPrice.Currency);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during creation of PaymentIntend with error {error}", ex.Message);
            throw;
        }
    }
}
