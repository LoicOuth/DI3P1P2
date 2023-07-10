namespace Infrastructure.Stripe;

public class StripeProductHelper : IProductHelper
{
    private readonly ProductService _productService = new();
    private readonly PriceService _priceService = new ();

    public async Task<Product> GetProductAsync(string productId, params string[]? expend)
    {
        var options = new ProductGetOptions();

        if(expend != null && expend.Length != 0)
            expend!.ToList().ForEach(value => options.AddExpand(value));

        return await _productService.GetAsync(productId, options);
    }

    public async Task<Product> UpdateProductPrice(string productId, long amount, string currency)
    {
        var product = await GetProductAsync(productId, "default_price");
        var price = await _priceService.CreateAsync(new PriceCreateOptions { UnitAmount = amount, Currency = currency, Product = product.Id });
        await _productService.UpdateAsync(productId,
            new ProductUpdateOptions
            {
                DefaultPrice = price.Id
            });
        await _priceService.UpdateAsync(product.DefaultPrice.Id, new PriceUpdateOptions { Active = false });
        return await GetProductAsync(productId, "default_price");
    }
}
