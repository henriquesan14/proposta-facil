namespace PropostaFacil.Domain.Tenants.Contracts;

public interface ITenantRuleCheck
{
    bool TenantEmailExists(string email);
    bool TenantDocumentExists(string document);
}
