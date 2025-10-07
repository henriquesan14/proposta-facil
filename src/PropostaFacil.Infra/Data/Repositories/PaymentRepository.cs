using Microsoft.EntityFrameworkCore;
using PropostaFacil.Application.Payments;
using PropostaFacil.Domain.Payments;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories;

public class PaymentRepository : NoSaveSoftDeleteEfRepository<Payment, PaymentId>, IPaymentRepository
{
    public PaymentRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
    }

    public async Task SoftDeleteBySubscriptionId(SubscriptionId subscriptionId)
    {
        await DbContext.Payments
            .Where(p => p.SubscriptionId == subscriptionId && p.IsActive)
            .ExecuteUpdateAsync(p => p.SetProperty(x => x.IsActive, false));
    }
}
