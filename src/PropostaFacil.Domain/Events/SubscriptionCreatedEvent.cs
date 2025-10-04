using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Subscriptions;

namespace PropostaFacil.Domain.Events;

public record SubscriptionCreatedEvent(Subscription Subscription) : IDomainEvent;
