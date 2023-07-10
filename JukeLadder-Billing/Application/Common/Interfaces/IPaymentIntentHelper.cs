namespace Application.Common.Interfaces;

public interface IPaymentIntentHelper
{
    Task<Stripe.PaymentIntent> CreateAsync(long amount, string currency, string description, string franchiseId, string title, string artist, string album, string cover, int duration, string deezerId, CancellationToken cancellationToken = default);
    Task<Charge?> GetChargeFromPaymentIntendAsync(string paymentIntentId, CancellationToken cancellationToken = default);
    Task<bool> RefundChargeAsync(string chargeId, CancellationToken cancellationToken = default);
}
