using MediatR;

namespace PropostaFacil.Shared.Common.CQRS
{
    public interface IQuery<out TResponse> : IRequest<TResponse>
    where TResponse : notnull
    {
    }
}
