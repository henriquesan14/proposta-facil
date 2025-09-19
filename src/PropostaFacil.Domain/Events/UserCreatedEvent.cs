using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Users;

namespace PropostaFacil.Domain.Events;

public record UserCreatedEvent(User User) : IDomainEvent;
