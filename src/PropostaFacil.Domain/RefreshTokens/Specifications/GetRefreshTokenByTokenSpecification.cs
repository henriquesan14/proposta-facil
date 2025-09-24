using Ardalis.Specification;

namespace PropostaFacil.Domain.RefreshTokens.Specifications;

public class GetRefreshTokenByTokenSpecification : SingleResultSpecification<RefreshToken>
{
    public GetRefreshTokenByTokenSpecification(string refreshToken)
    {
        Query
            .Where(rt => rt.Token == refreshToken);
    }
}
