using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Payments;
using PropostaFacil.Domain.Subscriptions;

namespace PropostaFacil.Domain.Events;

public record PaymentApprovedEvent(Payment Payment, Subscription Subscription) : IDomainEvent;
