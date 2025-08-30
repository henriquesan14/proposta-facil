using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Domain.Events
{
    public record PaymentApprovedEvent(Payment Payment, Subscription Subscription) : IDomainEvent;
}
