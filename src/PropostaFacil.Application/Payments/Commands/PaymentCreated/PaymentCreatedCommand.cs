using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Payments.Commands.PaymentCreated;

public record PaymentCreatedCommand(string @Event, PaymentAsaas Payment) : ICommand<Result>;
