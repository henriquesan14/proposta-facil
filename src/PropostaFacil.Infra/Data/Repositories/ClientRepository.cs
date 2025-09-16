using Ardalis.Specification.EntityFrameworkCore;
using PropostaFacil.Application.Clients;
using PropostaFacil.Domain.Clients;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}
