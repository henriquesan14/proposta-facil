using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Clients;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.Events;
using PropostaFacil.Domain.Payments;
using PropostaFacil.Domain.Tenants;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Proposals
{
    public class Proposal : Aggregate<ProposalId>
    {
        private readonly List<ProposalItem> _items = new();
        private string _currency = default!;
        private Money _totalAmount = default!;
        public static Proposal Create(TenantId tenantId, ClientId clientId, string title, string currency, DateTime validUntil)
        {
            return new Proposal {
                Id = ProposalId.Of(Guid.NewGuid()),
                TenantId = tenantId,
                ClientId = clientId,
                Number = GenerateProposalNumber(tenantId, clientId),
                Title = title,
                ProposalStatus = ProposalStatusEnum.Draft,
                _currency  = currency,
                _totalAmount = Money.Of(0, currency),
                ValidUntil = validUntil
            };
        }

        public TenantId TenantId { get; private set; } = default!;
        public ClientId ClientId { get; private set; } = default!;
        public string Number { get; private set; } = default!;
        public string Title { get; private set; } = default!;
        public ProposalStatusEnum ProposalStatus { get; private set; } = default!;
        public Money TotalAmount => _totalAmount;
        public DateTime ValidUntil { get; private set; } = default!;
        public Tenant Tenant { get; private set; } = default!;
        public Client Client { get; private set; } = default!;

        public Payment? Payment { get; private set; } = default!;

        public IReadOnlyCollection<ProposalItem> Items => _items.AsReadOnly();

        public void SendProposal()
        {
            ProposalStatus = ProposalStatusEnum.Sent;
            AddDomainEvent(new ProposalSentEvent(this));
        }

        public void AddItem(ProposalItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _items.Add(item);
            RecalculateTotal();
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == ProposalItemId.Of(itemId));
            if (item == null) throw new ArgumentNullException(nameof(item));

            _items.Remove(item);
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            _totalAmount = Money.Of(_items.Sum(i => i.TotalPrice), _currency);
        }

        private static string GenerateProposalNumber(TenantId tenantId, ClientId clientId)
        {
            var datePart = DateTime.Now.ToString("ddMMyy-HHmmss");
            var tenantPart = tenantId.Value.ToString("N").Substring(0, 4).ToUpper();
            var clientPart = clientId.Value.ToString("N").Substring(0, 4).ToUpper();
            return $"{datePart}-{tenantPart}-{clientPart}";
        }
    }
}
