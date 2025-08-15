namespace PropostaFacil.Domain.ValueObjects
{
    public record Money
    {
        public decimal Amount { get; init; }
        public string Currency { get; init; }

        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static Money Of(decimal amount, string currency) {
            if (string.IsNullOrWhiteSpace(currency))
                throw new ArgumentException("Currency é obrigatório.");
            return new Money(amount, currency);
        }

        public override string ToString()
        {
            return string.Format("{0:N2} {1}", Amount, Currency);
        }
    }
}
