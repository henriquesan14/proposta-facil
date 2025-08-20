namespace PropostaFacil.Application.Subscriptions
{
    public record SubscriptionPlanResponse(Guid Id, string Name, int MaxProposalsPerMonth, decimal Price, string Description);
}
