using MassTransit;

namespace Application.Saga;

public class ForceAddTrackResponse : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public bool Error { get; set; }
    public string PaymentIntendId { get; set; }
    public ForceAddTrackResponse(bool error, string paymentIntendId)
    {
        Error = error;
        PaymentIntendId = paymentIntendId;
    }
}
