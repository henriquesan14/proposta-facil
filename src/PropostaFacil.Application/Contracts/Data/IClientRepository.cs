using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Contracts.Data
{
    public interface IClientRepository : IAsyncRepository<Client, ClientId>;
}
