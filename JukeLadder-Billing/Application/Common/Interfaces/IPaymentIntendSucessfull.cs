﻿using MassTransit;

namespace Application.Common.Interfaces;

public class PaymentIntendSucessfull : CorrelatedBy<Guid>
{
    public Guid CorrelationId { get; set; }
    public string Id { get; set; }
    public string FranchiseId { get; set; }
    public string Title { get; set; }
    public string Artist { get; set; }
    public string Album { get; set; }
    public string Cover { get; set; }
    public int Duration { get; set; }
    public string DeezerId { get; set; }
    public string PaymentIntendId { get; set; }
}
