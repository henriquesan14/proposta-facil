using PropostaFacil.Application.Proposals;
using PropostaFacil.Application.Proposals.Commands.CreateProposal;
using PropostaFacil.Domain.Enums;

namespace PropostaFacil.Tests.Builders.Commands
{
    public class CreateProposalCommandBuilder
    {
        private Guid _clientId = Guid.NewGuid();
        private string _title = "teste";
        private ProposalStatusEnum _proposalStatus = ProposalStatusEnum.Draft;
        private string _currency = "BRL";
        private DateTime _validUntil = DateTime.Now;
        private IEnumerable<ProposalItemRequest> _items = new List<ProposalItemRequest>
        {
            new ProposalItemRequest("teste", "teste", 1, 10),
            new ProposalItemRequest("teste", "teste", 1, 10)
        };

        public CreateProposalCommand Build()
            => new CreateProposalCommand(_clientId, _title, _proposalStatus, _currency, _validUntil, _items);

        public CreateProposalCommandBuilder WithClientId(Guid clientId)
        {
            _clientId = clientId;
            return this;
        }

        public CreateProposalCommandBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public CreateProposalCommandBuilder WithStatus(ProposalStatusEnum status)
        {
            _proposalStatus = status;
            return this;
        }

        public CreateProposalCommandBuilder WithCurrency(string currency)
        {
            _currency = currency;
            return this;
        }

        public CreateProposalCommandBuilder WithValidUntil(DateTime validUntil)
        {
            _validUntil = validUntil;
            return this;
        }

        public CreateProposalCommandBuilder WithItems(IEnumerable<ProposalItemRequest> items)
        {
            _items = items;
            return this;
        }
    }
}
