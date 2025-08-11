namespace PropostaFacil.Application.Tenants
{
    public record TenantResponse(Guid Id, string Name, string Cnpj, string Domain);
}
