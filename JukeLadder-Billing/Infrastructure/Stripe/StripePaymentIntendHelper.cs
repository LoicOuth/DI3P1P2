using System.Net;

namespace Infrastructure.Stripe;

public class StripePaymentIntendHelper : IPaymentIntentHelper
{
    private readonly PaymentIntentService _paymentIntentService;
    private readonly ChargeService _chargeService;
    private readonly RefundService _refundService;

    public StripePaymentIntendHelper(PaymentIntentService paymentIntentService, ChargeService chargeService, RefundService refundService)
    {
        _paymentIntentService = paymentIntentService;
        _chargeService = chargeService;
        _refundService = refundService;
    }

    public Task<PaymentIntent> CreateAsync(long amount, string currency, string description, string franchiseId, string title, string artist, string album, string cover, int duration, string deezerId, CancellationToken cancellationToken = default)
    {
        return _paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
        {
            Amount = amount,
            Currency = currency,
            AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
            {
                Enabled = true,
            },
            Description = description,
            Metadata = new Dictionary<string, string>
            {
                {"Amount", amount.ToString()},
                {"Currency", currency},
                {"Description", description},
                {"FranchiseId", franchiseId},
                {"Title", title},
                {"Artist", artist},
                {"Album", album},
                {"Cover", cover},
                {"Duration", duration.ToString()},
                {"DeezerId", deezerId},

            }
        }, cancellationToken: cancellationToken);
    }

    public async Task<Charge?> GetChargeFromPaymentIntendAsync(string paymentIntentId, CancellationToken cancellationToken = default)
    {
        var charge = await _chargeService.ListAsync(new ChargeListOptions
        {
            PaymentIntent = paymentIntentId
        }, cancellationToken: cancellationToken);

        return charge.ToList().FirstOrDefault();
    }

    public async Task<bool> RefundChargeAsync(string chargeId, CancellationToken cancellationToken = default)
    {
        var refund = await _refundService.CreateAsync(new RefundCreateOptions
        {
            Charge = chargeId
        }, cancellationToken: cancellationToken);

        return refund.StripeResponse.StatusCode.Equals(HttpStatusCode.OK);
    }
}