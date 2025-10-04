using PropostaFacil.Application.Proposals.Queries.GetProposals;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Tests.Builders.Queries;

public class GetProposalsQueryBuilder
{
    private string _title = "title";
    private string _number = "12345666";
    private string _documentClient = "12345666";
    private ProposalStatusEnum _proposalStatus = ProposalStatusEnum.Draft;

    public GetProposalsQuery Build()
        => new GetProposalsQuery(_documentClient, _title, _number, _proposalStatus);

    public GetProposalsQueryBuilder WithTitle(string title)
    {
        _title = title;
        return this;
    }
    public GetProposalsQueryBuilder WithNumber(string number)
    {
        _number = number;
        return this;
    }

    public GetProposalsQueryBuilder WithProposalStatus(ProposalStatusEnum proposalStatus)
    {
        _proposalStatus = proposalStatus;
        return this;
    }
}
