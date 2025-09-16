using PropostaFacil.Application.Payments;
using PropostaFacil.Domain.Payments;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class PaymentRepository : NoSaveEfRepository<Payment, PaymentId>, IPaymentRepository
    {
        public PaymentRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}
