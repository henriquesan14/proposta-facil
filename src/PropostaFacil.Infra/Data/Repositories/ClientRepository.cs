using PropostaFacil.Application.Clients;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories;

public class ClientRepository : NoSaveSoftDeleteEfRepository<Client, ClientId>, IClientRepository
{
    public ClientRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
    }
}
