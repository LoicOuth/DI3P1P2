namespace Domain.Entities;

public class BillingState
{
    public Guid Id { get; set; }
    public string FranchiseId { get; set; }
    public bool Enable { get; set; }
}
