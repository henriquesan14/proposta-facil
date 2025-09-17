using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Domain.Clients.Contracts;

public interface IClientRuleCheck
{
    bool DocumentExists(Document document);
    bool EmailExists(string email);
}
