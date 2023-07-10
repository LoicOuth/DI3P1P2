using Application.Common.Interfaces;
using Application.PaymentIntent.Command.PaymentIntentRefundCommand;
using Application.PaymentIntent.Helpers;
using MassTransit;

namespace Application.Saga;

public class PaymentStateMachine : MassTransitStateMachine<PaymentSaga>
{
    private readonly PaymentIntentHelper _paymentIntentHelper;
    public Event<PaymentIntendSucessfull> PaymentIntentSuccessfull { get; private set; }
    public Request<PaymentSaga, ForceAddTrackRequest, ForceAddTrackResponse> ForceAddTrack { get; private set; }

    public PaymentStateMachine(ILogger<PaymentStateMachine> logger, PaymentIntentHelper paymentIntentHelper)
    {
        _paymentIntentHelper = paymentIntentHelper;

        InstanceState(instance => instance.CurrentState);

        Event(() => PaymentIntentSuccessfull, instance => instance
               .CorrelateBy<Guid>(state => state.CorrelationId, context => context.Message.CorrelationId)
               .SelectId(s => Guid.NewGuid()));

        Request(() => ForceAddTrack, r => { r.Timeout = new TimeSpan(0, 0, 50); });

        Initially(
            When(PaymentIntentSuccessfull)
                .Request(ForceAddTrack, x =>
                {
                    var context = (ConsumeContext<PaymentIntendSucessfull>)x;
                    
                    return x.Init<ForceAddTrackRequest>(new ForceAddTrackRequest()
                    {
                        Id = context.Message.Id,
                        FranchiseId = context.Message.FranchiseId,
                        Title = context.Message.Title,
                        Artist = context.Message.Artist,
                        Album = context.Message.Album,
                        Cover = context.Message.Cover,
                        Duration = context.Message.Duration,
                        DeezerId = context.Message.DeezerId,
                        PaymentIntendId = context.Message.PaymentIntendId,
                        CorrelationId = context.CorrelationId!.Value
                    });
                })
                .Then(x =>
                {
                    logger.LogInformation("Saga Initiated with ID : {ID} ", x.Message.CorrelationId.ToString());
                })
                .TransitionTo(ForceAddTrack?.Pending));

        During(ForceAddTrack?.Pending,
            Ignore(PaymentIntentSuccessfull));

        During(ForceAddTrack?.Pending,
            When(ForceAddTrack?.Faulted)
                .Then(context =>
                {
                    Refund(context.Message.Message.PaymentIntendId);
                    logger.LogError("Error during ForceAddTrack for track: {trackId}", context.Saga.Id);
                }).Finalize(),
            When(ForceAddTrack?.Completed)
                .Then(context =>
                {
                    if (context.Message.Error)
                    {
                        Refund(context.Message.PaymentIntendId);
                        logger.LogError("Error during ForceAddTrack for track: {trackId}", context.Saga.Id);
                    }
                    else
                        logger.LogInformation("ForceAddTrack completed");
                }));
    }

    private async void Refund(string paymentIntendId)
    {
        await _paymentIntentHelper.RefundPaymentIntend(paymentIntendId, CancellationToken.None);
    }
}

