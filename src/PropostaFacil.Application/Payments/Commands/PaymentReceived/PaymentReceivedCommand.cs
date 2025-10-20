using Common.ResultPattern;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Payments.Commands.PaymentReceived;

public record PaymentReceivedCommand(string @Event, PaymentAsaas Payment) : ICommand<Result>;
