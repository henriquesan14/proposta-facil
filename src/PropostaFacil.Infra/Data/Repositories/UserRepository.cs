using PropostaFacil.Application.Users;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Infra.Data.Repositories;

public class UserRepository : NoSaveEfRepository<User, UserId>, IUserRepository
{
    public UserRepository(PropostaFacilDbContext dbContext) : base(dbContext)
    {
    }
}
