using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Domain.Events
{
    public record SubscriptionCreatedEvent(Subscription Subscription) : IDomainEvent;
}
