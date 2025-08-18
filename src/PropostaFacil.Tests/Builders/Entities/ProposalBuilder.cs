using PropostaFacil.Domain.Entities;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Tests.Builders.Entities
{
    public class ProposalBuilder
    {
        private TenantId _tenantId = TenantId.Of(Guid.NewGuid());
        private ClientId _clientId = ClientId.Of(Guid.NewGuid());
        private string _number = "PROP-001";
        private string _title = "Proposal Test";
        private ProposalStatusEnum _status = ProposalStatusEnum.Draft;
        private string _currency = "BRL";
        private DateTime _validUntil = DateTime.UtcNow.AddDays(30);
        private readonly List<ProposalItem> _items = new();

        public ProposalBuilder WithTenantId(TenantId tenantId)
        {
            _tenantId = tenantId;
            return this;
        }

        public ProposalBuilder WithClientId(ClientId clientId)
        {
            _clientId = clientId;
            return this;
        }

        public ProposalBuilder WithNumber(string number)
        {
            _number = number;
            return this;
        }

        public ProposalBuilder WithTitle(string title)
        {
            _title = title;
            return this;
        }

        public ProposalBuilder WithStatus(ProposalStatusEnum status)
        {
            _status = status;
            return this;
        }

        public ProposalBuilder WithCurrency(string currency)
        {
            _currency = currency;
            return this;
        }

        public ProposalBuilder WithValidUntil(DateTime validUntil)
        {
            _validUntil = validUntil;
            return this;
        }

        public ProposalBuilder AddItem(ProposalItem item)
        {
            _items.Add(item);
            return this;
        }

        public Proposal Build()
        {
            var proposal = Proposal.Create(
                _tenantId,
                _clientId,
                _title,
                _status,
                _currency,
                _validUntil
            );

            // adicionar items se houver
            foreach (var item in _items)
            {
                proposal.AddItem(item);
            }

            return proposal;
        }
    }

}
