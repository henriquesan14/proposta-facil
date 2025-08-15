using PropostaFacil.Application.Users;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User, UserId>, IUserRepository
    {
        public UserRepository(PropostaFacilDbContext dbContext) : base(dbContext)
        {
        }
    }
}
