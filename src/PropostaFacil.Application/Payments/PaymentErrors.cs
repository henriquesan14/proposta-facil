using Common.ResultPattern;

namespace PropostaFacil.Application.Payments
{
    public class PaymentErrors
    {
        public static Error InvalidEvent() =>
            Error.Validation("Payments.Validation", $"Invalid event for this subscriptions.");

        public static Error SubscriptionAlreadyActive() =>
            Error.Validation("Payments.Validation", $"Subscription already active.");

        public static Error NotFound(string id) =>
            Error.NotFound("Payments.NotFound", $"Subscription with {id} not found.");

        public static Error PaymentAlreadyExist(string id) =>
            Error.Conflict("Payments.Conflict", $"Already exists payment with {id}.");
    }
}
