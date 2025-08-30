using PropostaFacil.Application.Payments;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class PaymentRepository : RepositoryBase<Payment, PaymentId>, IPaymentRepository
    {
        public PaymentRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}
