using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Users
{
    public interface IUserRepository : IAsyncRepository<User, UserId>;
}
