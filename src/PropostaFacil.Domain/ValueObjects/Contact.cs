namespace PropostaFacil.Domain.ValueObjects
{
    public record Contact
    {
        public string Email { get; init; }
        public string PhoneNumber { get; init; }

        private Contact(string email, string phoneNumber)
        {
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public static Contact Of(string email, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
                throw new ArgumentException("E-mail inválido.");
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Telefone é obrigatório.");

            return new Contact(email, phoneNumber);
        }
    }
}
