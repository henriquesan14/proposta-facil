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
                throw new ArgumentException("Telefone é obrigatório.");
            return new Money(amount, currency);
        }
    }
}
