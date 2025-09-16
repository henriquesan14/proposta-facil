using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients.Contracts;
using PropostaFacil.Domain.Clients.Specifications;
using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Infra.Services;

public class RuleCheckService(IUnitOfWork unitOfWork) : IClientRuleCheck
{
    public bool DocumentExists(Document document)
    {
        return unitOfWork.Clients.AnyAsync(new GetClientByDocumentGlobalSpecification(document.Number)).GetAwaiter().GetResult();
    }

    public bool EmailExists(string email)
    {
        return unitOfWork.Clients.AnyAsync(new GetClientByEmailGlobalSpecification(email)).GetAwaiter().GetResult();
    }
}
