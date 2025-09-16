using Ardalis.Specification;
using PropostaFacil.Domain.Abstractions;

namespace PropostaFacil.Domain.Specifications;

public class GlobalSpecification<T, TId> : Specification<T> where T : Aggregate<TId>
{
    protected GlobalSpecification()
    {
        Query
            .IgnoreQueryFilters();
    }
}
