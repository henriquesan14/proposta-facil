using Common.ResultPattern;
using PropostaFacil.Application.Shared.Interfaces;
using PropostaFacil.Domain.Clients.Specifications;
using PropostaFacil.Domain.ValueObjects.Ids;
using PropostaFacil.Shared.Common.CQRS;

namespace PropostaFacil.Application.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<DeleteClientCommand, Result>
{
    public async  Task<Result> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await unitOfWork.Clients.SingleOrDefaultAsync(new GetClientByIdSpecification(ClientId.Of(request.Id)));
        if (client == null) return ClientErrors.NotFound(request.Id);

        unitOfWork.Clients.SoftDelete(client);
        await unitOfWork.CompleteAsync();

        return Result.Success();
    }
}
