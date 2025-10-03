using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Payments.Commands.PaymentOverdue;

public record PaymentOverdueCommand(string @Event, PaymentAsaas Payment) : ICommand<Result>;
