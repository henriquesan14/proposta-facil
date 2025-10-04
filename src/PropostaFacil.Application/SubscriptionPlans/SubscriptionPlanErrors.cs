using Common.ResultPattern;

namespace PropostaFacil.Application.SubscriptionPlans;

public class SubscriptionPlanErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("SubscriptionPlans.NotFound", $"SubscriptionPlan with Id: {id} not found");

    public static Error Conflict(string name) =>
        Error.Conflict("SubscriptionPlans.Conflict", $"SubscriptionPlan with email: {name} already exists");

    public static Error Forbidden() =>
        Error.AccessForbidden("SubscriptionPlans.Forbidden", $"This user does not have permission to do this.");
}
