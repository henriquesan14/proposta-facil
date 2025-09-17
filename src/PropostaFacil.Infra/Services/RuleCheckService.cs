using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients.Contracts;
using PropostaFacil.Domain.Clients.Specifications;
using PropostaFacil.Domain.Users.Contracts;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Infra.Services;

public class RuleCheckService(IUnitOfWork unitOfWork) : IClientRuleCheck, IUserRuleCheck
{
    public bool ClientDocumentExists(Document document)
    {
        return unitOfWork.Clients.AnyAsync(new GetClientByDocumentGlobalSpecification(document.Number)).GetAwaiter().GetResult();
    }

    public bool ClientEmailExists(string email)
    {
        return unitOfWork.Clients.AnyAsync(new GetClientByEmailGlobalSpecification(email)).GetAwaiter().GetResult();
    }

    public bool UserEmailExists(string email)
    {
        return unitOfWork.Users.AnyAsync(new GetUserByEmailGlobalSpecification(email)).GetAwaiter().GetResult();
    }
}
