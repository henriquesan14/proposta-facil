using PropostaFacil.Application.Clients;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class ClientRepository : RepositoryBase<Client, ClientId>, IClientRepository
    {
        public ClientRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}
