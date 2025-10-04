namespace PropostaFacil.Application.SubscriptionPlans;

public record SubscriptionPlanResponse(Guid Id, string Name, int MaxProposalsPerMonth, decimal Price, string Description);
