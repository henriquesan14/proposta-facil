using Ardalis.Specification;
using PropostaFacil.Domain.Abstractions;

namespace PropostaFacil.Domain.Specifications;
public abstract class GlobalSingleResultSpecification<T, TId> : SingleResultSpecification<T> where T : Aggregate<TId>
{
    protected GlobalSingleResultSpecification()
    {
        Query
            .IgnoreQueryFilters();
    }
}
