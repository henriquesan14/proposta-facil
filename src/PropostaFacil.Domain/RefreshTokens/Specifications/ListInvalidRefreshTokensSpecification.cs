using Ardalis.Specification;

namespace PropostaFacil.Domain.RefreshTokens.Specifications;

public class ListInvalidRefreshTokensSpecification : Specification<RefreshToken>
{
    public ListInvalidRefreshTokensSpecification()
    {
        var now = DateTime.Now;
        Query
            .Where(t => t.ExpiresAt <= now || t.RevokedAt != null);
    }
}
