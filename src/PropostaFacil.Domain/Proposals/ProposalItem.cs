using PropostaFacil.Domain.Abstractions;
using PropostaFacil.Domain.ValueObjects.Ids;

namespace PropostaFacil.Domain.Proposals
{
    public class ProposalItem : Aggregate<ProposalItemId>
    {
        public static ProposalItem Create(string name, string description, int quantity, decimal unitPrice)
        {
            return new ProposalItem {
                Id = ProposalItemId.Of(Guid.NewGuid()),
                Name = name,
                Description = description,
                Quantity = quantity,
                UnitPrice = unitPrice
            };
        }

        public void Update(string name, string description, int quantity, decimal unitPrice)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        public ProposalId ProposalId { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Description { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
        public decimal UnitPrice { get; private set; } = default!;
        public decimal TotalPrice => Quantity * UnitPrice;

        public Proposal Proposal { get; private set; } = default!;
    }
}
