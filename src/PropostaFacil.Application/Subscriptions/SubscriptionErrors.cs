using Common.ResultPattern;

namespace PropostaFacil.Application.Subscriptions;

public class SubscriptionErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Subscriptions.NotFound", $"Subscription with Id: {id} not found");

    public static Error NoSubscriptionTenant() =>
        Error.NotFound("Subscriptions.NoSubscriptionTenant", $"No subscriptions on this tenant");

    public static Error Conflict() =>
        Error.Conflict("Subscriptions.Conflict", $"Already exist a subscription this tenant");

    public static Error Forbidden() =>
        Error.AccessForbidden("Subscriptions.Forbidden", $"This user does not have permission to do this.");

    public static Error InactiveSubscription() =>
        Error.Validation("Subscriptions.Validation", $"This tenant does not have an active subscription.");

    public static Error SubscriptionLimit() =>
        Error.Validation("Subscriptions.Validation", $"This tenant have already reached the subscription proposal limit.");

    public static Error ProposalAlreadySent() =>
        Error.Validation("Subscriptions.Validation", $"this proposal has already been sent.");
}
