using Common.ResultPattern;

namespace PropostaFacil.Application.Subscriptions
{
    public class SubscriptionErrors
    {
        public static Error NotFound(Guid id) =>
            Error.NotFound("Subscriptions.NotFound", $"SubscriptionPlan with Id: {id} not found");

        public static Error Conflict() =>
            Error.Conflict("Subscriptions.Conflict", $"Already exist subscription active this tenant");

        public static Error Forbidden() =>
            Error.AccessForbidden("Subscriptions.Forbidden", $"This user does not have permission to do this.");
    }
}
