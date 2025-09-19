using PropostaFacil.Domain.ValueObjects;

namespace PropostaFacil.Domain.Clients.Contracts;

public interface IClientRuleCheck
{
    bool ClientDocumentExists(Document document);
    bool ClientEmailExists(string email);
}
