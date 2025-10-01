using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients.Contracts;
using PropostaFacil.Domain.Clients.Specifications;
using PropostaFacil.Domain.SubscriptionPlans.Contracts;
using PropostaFacil.Domain.SubscriptionPlans.Specifications;
using PropostaFacil.Domain.Tenants.Contracts;
using PropostaFacil.Domain.Tenants.Specifications;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Infra.Services;

public class RuleCheckService(IUnitOfWork unitOfWork) : IClientRuleCheck, IUserRuleCheck, ITenantRuleCheck, ISubscriptionPlanRuleCheck
{
    public bool ClientDocumentExists(Document document)
    {
        return unitOfWork.Clients.AnyAsync(new GetClientByDocumentGlobalSpecification(document.Number)).GetAwaiter().GetResult();
    }

    public bool ClientEmailExists(string email)
    {
        return unitOfWork.Clients.AnyAsync(new GetClientByEmailGlobalSpecification(email)).GetAwaiter().GetResult();
    }

    public bool TenantEmailExists(string email)
    {
        return unitOfWork.Tenants.AnyAsync(new GetTenantByEmailGlobalSpecification(email)).GetAwaiter().GetResult();
    }

    public bool TenantDocumentExists(string document)
    {
        return unitOfWork.Tenants.AnyAsync(new GetTenantByDocumentGlobalSpecification(document)).GetAwaiter().GetResult();
    }

    public bool UserEmailExists(string email)
    {
        return unitOfWork.Users.AnyAsync(new GetUserByEmailGlobalSpecification(email)).GetAwaiter().GetResult();
    }

    public bool SubscriptionPlanNameExists(string subscriptionPlanName)
    {
        return unitOfWork.SubscriptionPlans.AnyAsync(new GetSubscriptionPlanByNameGlobalSpecification(subscriptionPlanName)).GetAwaiter().GetResult();
    }
}
