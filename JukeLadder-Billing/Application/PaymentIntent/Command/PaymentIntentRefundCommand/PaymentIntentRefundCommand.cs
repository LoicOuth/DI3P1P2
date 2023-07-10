namespace Application.PaymentIntent.Command.PaymentIntentRefundCommand;

public class PaymentIntentRefundCommand : IRequest
{
    public string PaymentIntendId { get; set; }
	public PaymentIntentRefundCommand(string paymentIntendId)
	{
		PaymentIntendId = paymentIntendId;
	}
}
