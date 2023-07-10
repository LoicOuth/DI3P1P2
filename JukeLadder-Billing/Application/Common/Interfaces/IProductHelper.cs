namespace Application.Common.Interfaces;

public interface IProductHelper
{
    Task<Stripe.Product> GetProductAsync(string productId, params string[]? expend);
    Task<Stripe.Product> UpdateProductPrice(string productId, long amount, string currency);
}
