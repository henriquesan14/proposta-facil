using Ardalis.Specification;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Clients;

public interface IClientRepository : IReadRepositoryBase<Client>, INoSaveSoftDeleteEfRepository<Client, ClientId>;
