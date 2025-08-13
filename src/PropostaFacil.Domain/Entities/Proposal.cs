using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.Enums;
using PropostaFacil.Domain.ValueObjects;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Entities
{
    public class Proposal : Aggregate<ProposalId>
    {
        public TenantId TenantId { get; private set; } = default!;
        public ClientId ClientId { get; private set; } = default!;
        public string Number { get; private set; } = default!;
        public string Title { get; private set; } = default!;
        public ProposalStatusEnum ProposalStatus { get; private set; } = default!;
        public Money TotalAmount { get; private set; } = default!;
        public DateTime ValidUntil { get; private set; } = default!;

        public Tenant Tenant { get; private set; } = default!;
        public Client Client { get; private set; } = default!;

        private readonly List<ProposalItem> _items = new();
        public IReadOnlyCollection<ProposalItem> Items => _items.AsReadOnly();


        public void AddItem(ProposalItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _items.Add(item);
        }

        public void RemoveItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == ProposalItemId.Of(itemId));
            if (item == null) throw new ArgumentNullException(nameof(item));

            _items.Remove(item);
        }
    }
}
