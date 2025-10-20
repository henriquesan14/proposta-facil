using Ardalis.Specification;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Tenants;

public interface ITenantRepository : IReadRepositoryBase<Tenant>, INoSaveSoftDeleteEfRepository<Tenant, TenantId>;
