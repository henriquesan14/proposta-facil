using Ardalis.Specification;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Payments;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Payments;

public interface IPaymentRepository : IReadRepositoryBase<Payment>, INoSaveEfRepository<Payment, PaymentId>;
