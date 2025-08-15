using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Clients
{
    public interface IClientRepository : IAsyncRepository<Client, ClientId>;
}
