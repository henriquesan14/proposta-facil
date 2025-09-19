using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Application.Shared.Request;
using PropostaFacil.Application.Tenants;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Events;
using PropostaFacil.Domain.Subscriptions;
using PropostaFacil.Domain.Subscriptions.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Subscriptions.Commands.CreateSubscription
{
    public class CreateSubscriptionCommandHandler(IUnitOfWork unitOfWork, IAsaasService asaasService) : ICommandHandler<CreateSubscriptionCommand, ResultT<SubscriptionResponse>>
    {
        public async Task<ResultT<SubscriptionResponse>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var hasSubscriptionActive = await unitOfWork.Subscriptions.SingleOrDefaultAsync(new GetSubscriptionsByStatusSpecification(SubscriptionStatusEnum.Active));
            if (hasSubscriptionActive != null) return SubscriptionErrors.Conflict();

            var tenant = await unitOfWork.Tenants.GetByIdAsync(TenantId.Of(request.TenantId));
            if (tenant == null)
                return TenantErrors.NotFound(request.TenantId);
            var subscriptionPlan = await unitOfWork.SubscriptionPlans.GetByIdAsync(SubscriptionPlanId.Of(request.SubscriptionPlanId));
            if (subscriptionPlan == null)
                return SubscriptionPlanErrors.NotFound(request.SubscriptionPlanId);

            var subscriptionAsaas = new CreateSubscriptionRequest(tenant.AsaasId, request.BillingType, subscriptionPlan.Price,  request.StartDate, "MONTHLY", subscriptionPlan.Description);

            var responseSubscriptionAsaas = await asaasService.CreateSubscriptionAsync(subscriptionAsaas);

            var payments = await asaasService.GetPaymentsBySubscription(responseSubscriptionAsaas.Id);

            var firstPayment = payments.Data.FirstOrDefault();

            var subscription = Subscription.Create(TenantId.Of(request.TenantId), SubscriptionPlanId.Of(request.SubscriptionPlanId), request.StartDate, responseSubscriptionAsaas.Id,
                firstPayment!.InvoiceUrl, request.EndDate);

            await unitOfWork.Subscriptions.AddAsync(subscription);
            await unitOfWork.CompleteAsync();

            return subscription.ToDto();
        }
    }
}
