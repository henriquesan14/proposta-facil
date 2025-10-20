using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Domain.Proposals.Rules;

internal class ProposalMustBeInDraftRule : IBusinessRule
{
    private readonly ProposalStatusEnum _status;

    public ProposalMustBeInDraftRule(ProposalStatusEnum status)
    {
        _status = status;
    }

    public string Message => "A proposta só pode ser atualizada quando estiver em rascunho.";

    public bool IsBroken()
    {
        return _status != ProposalStatusEnum.Draft;
    }
}
