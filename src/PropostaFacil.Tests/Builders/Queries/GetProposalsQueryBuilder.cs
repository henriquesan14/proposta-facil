using PropostaFacil.Application.Proposals.Queries.GetProposals;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Tests.Builders.Queries
{
    public class GetProposalsQueryBuilder
    {
        private Guid _tenantId = Guid.NewGuid();
        private Guid _clientId = Guid.NewGuid();
        private string _title = "title";
        private string _number = "12345666";
        private ProposalStatusEnum _proposalStatus = ProposalStatusEnum.Draft;

        public GetProposalsQuery Build()
            => new GetProposalsQuery(_tenantId, _clientId, _title, _number, _proposalStatus);

        public GetProposalsQueryBuilder WithTenantId(Guid tenantId)
        {
            _tenantId = tenantId;
            return this;
        }

        public GetProposalsQueryBuilder WithClientId(Guid clientId)
        {
            _clientId = clientId;
            return this;
        }

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
}
