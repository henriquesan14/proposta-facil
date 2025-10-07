using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Queries.AdminGetUserById;

public class AdminGetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<AdminGetUserByIdQuery, ResultT<UserResponse>>
{
    public async Task<ResultT<UserResponse>> Handle(AdminGetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserByIdGlobalSpecification(UserId.Of(request.Id)));
        if (user == null) return UserErrors.NotFound(request.Id);

        return user.ToDto();
    }
}
