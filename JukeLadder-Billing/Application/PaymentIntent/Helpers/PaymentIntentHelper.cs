using Application.PaymentIntent.Command.PaymentIntentRefundCommand;

namespace Application.PaymentIntent.Helpers;

public class PaymentIntentHelper
{
    private readonly IPaymentIntentHelper _paymentIntentHelper;
    private readonly ILogger<PaymentIntentHelper> _logger;

    public PaymentIntentHelper(IPaymentIntentHelper paymentIntentHelper, ILogger<PaymentIntentHelper> logger)
    {
        _paymentIntentHelper = paymentIntentHelper;
        _logger = logger;
    }

    public async Task RefundPaymentIntend(string paymentIntendId, CancellationToken cancellationToken)
    {
        var charge = await _paymentIntentHelper.GetChargeFromPaymentIntendAsync(paymentIntendId, cancellationToken);

        if (charge == null)
            throw new BillingException($"Charge for PaymentIntent {paymentIntendId} not refunded, charge was not found");

        if (!await _paymentIntentHelper.RefundChargeAsync(charge.Id, cancellationToken))
            throw new BillingException($"Charge for PaymentIntent {paymentIntendId} not refunded, refund failed");

        _logger.LogInformation("Charge {ChargeId} refunded", charge.Id);
    }
}
