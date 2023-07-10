namespace Application.FeeManagement.Command.SetFeeCommand;

public record SetFeeCommand(long Min, long Max) : IRequest<string>;