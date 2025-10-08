using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Queries.GetClientById;

public class GetClientByIdQueryHandler(IUnitOfWork unitOfWork) : IQueryHandler<GetClientByIdQuery, ResultT<ClientResponse>>
{
    public async Task<ResultT<ClientResponse>> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await unitOfWork.Clients.SingleOrDefaultAsync(new GetClientByIdSpecification(ClientId.Of(request.Id)));
        if (client == null) return ClientErrors.NotFound(request.Id);

        return client.ToDto();
    }
}
