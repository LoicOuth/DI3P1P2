namespace Application.BillingStates.Commands.UpdateBillingStates;

public record UpdateBillingStatesCommand(string FranchiseId, bool Enable) : IRequest;