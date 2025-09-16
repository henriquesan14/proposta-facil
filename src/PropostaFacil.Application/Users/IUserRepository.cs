using Ardalis.Specification;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Users;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Application.Users;

public interface IUserRepository : IReadRepositoryBase<User>, INoSaveEfRepository<User, UserId>;
