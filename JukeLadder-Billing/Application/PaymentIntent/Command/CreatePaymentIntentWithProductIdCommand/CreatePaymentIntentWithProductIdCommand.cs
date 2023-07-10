using Application.PaymentIntent.Command.Dto;

namespace Application.PaymentIntent.Command.CreatePaymentIntentWithProductIdCommand;

public record CreatePaymentIntentWithProductIdCommand(string ProductId, string FranchiseId, string Title, string Artist, string Album, string Cover, int Duration, string DeezerId) : IRequest<PaymentIntentDto>;
