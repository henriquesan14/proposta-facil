using Ardalis.Specification.EntityFrameworkCore;
using PropostaFacil.Application.Users;
using PropostaFacil.Domain.Users;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}
