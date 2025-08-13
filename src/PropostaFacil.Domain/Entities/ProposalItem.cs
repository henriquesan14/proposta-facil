using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Entities
{
    public class ProposalItem : Aggregate<ProposalItemId>
    {
        public ProposalId ProposalId { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
        public decimal UnitPrice { get; private set; } = default!;
        public decimal TotalPrice => Quantity * UnitPrice;

        public Proposal Proposal { get; private set; } = default!;
    }
}
