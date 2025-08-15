using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Users.Queries.GetUsersByTenant
{
    public class GetUsersByTenantQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetUsersByTenantQuery, ResultT<IEnumerable<UserResponse>>>
    {
        public async Task<ResultT<IEnumerable<UserResponse>>> Handle(GetUsersByTenantQuery request, CancellationToken cancellationToken)
        {
            var users = await unitOfWork.Users.GetAsync(u => u.TenantId == TenantId.Of(request.TenantId));

            return users.ToDto();
        }
    }
}
