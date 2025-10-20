using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Users.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Queries.GetUserById;

public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetUserByIdQuery, ResultT<UserResponse>>
{
    public async  Task<ResultT<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.Users.SingleOrDefaultAsync(new GetUserByIdSpecification(UserId.Of(request.Id)));
        if (user == null) return UserErrors.NotFound(request.Id);

        return user.ToDto();
    }
}
