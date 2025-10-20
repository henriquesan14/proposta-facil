using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Users;

namespace PropostaFacil.Domain.Events;

public record ForgotPasswordEvent(User User) : IDomainEvent;

