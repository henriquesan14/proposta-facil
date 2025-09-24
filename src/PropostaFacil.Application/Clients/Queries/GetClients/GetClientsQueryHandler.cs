using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients.Specifications;
using PropostaFacil.Shared.Common.CQRS;
using PropostaFacil.Shared.Common.Pagination;

namespace PropostaFacil.Application.Clients.Queries.GetClients
{
    public class GetClientsQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetClientsQuery, ResultT<PaginatedResult<ClientResponse>>>
    {
        public async Task<ResultT<PaginatedResult<ClientResponse>>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            var spec = new ListClientsSpecification(request.Name, request.Document!);
            var paginated = await unitOfWork.Clients
                .ToPaginatedListAsync(spec, request.PageIndex, request.PageSize, u => u.ToDto());

            return paginated;
        }
    }
}
