using Common.ResultPattern;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Commands.CreateUser
{
    public record CreateUserCommand(string Name, string Email, string PhoneNumber, UserRoleEnum Role) : ICommand<ResultT<UserResponse>>;
}
