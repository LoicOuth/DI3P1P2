using Application.Product.Dto;

namespace Application.Product.Queries.GetProductByIdQuery;
public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductPriceDto>
{
    private readonly ILogger<GetProductByIdQuery> _logger;
    private readonly IProductHelper _productHelper;

    public GetProductByIdQueryHandler(ILogger<GetProductByIdQuery> logger, IProductHelper productHelper)
    {
        _logger = logger;
        _productHelper = productHelper;
    }

    public async Task<ProductPriceDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Get product {id}", request.ProductId);
            var product = await _productHelper.GetProductAsync(request.ProductId, "default_price");
            if (product == null)
                throw new NotFoundException(nameof(Stripe.Product), request.ProductId);
            if (product.DefaultPrice == null)
                throw new NotFoundException(nameof(Price), $"Price not found for product {request.ProductId}");

            return new ProductPriceDto(product.DefaultPrice.Id, product.DefaultPrice.UnitAmount!.Value, product.DefaultPrice.Currency);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error during getting product with id {productId} with {error}", request.ProductId, ex.Message);
            throw;
        }
    }
}