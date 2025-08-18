using PropostaFacil.Domain.Entities;

namespace PropostaFacil.Tests.Builders.Entities
{
    public class ProposalItemBuilder
    {
        private string _name = "Name";
        private string _description = "Description";
        private int _quantity = 1;
        private decimal _unitPrice = 12;

        public ProposalItem Build()
            => ProposalItem.Create(_name, _description, _quantity, _unitPrice);

        public ProposalItemBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public ProposalItemBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public ProposalItemBuilder WithQuantity(int quantity)
        {
            _quantity = quantity;
            return this;
        }

        public ProposalItemBuilder WithName(decimal unitPrice)
        {
            _unitPrice = unitPrice;
            return this;
        }
    }
}
