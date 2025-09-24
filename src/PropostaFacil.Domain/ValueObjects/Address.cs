namespace PropostaFacil.Domain.ValueObjects
{
    public record Address
    {
        public string Street { get; init; }
        public string Number { get; init; }
        public string? Complement { get; init; }
        public string District { get; init; }
        public string City { get; init; }
        public string State { get; init; }
        public string ZipCode { get; init; }

        private Address(string street, string number, string? complement, string district, string city, string state, string zipCode) {
            Street = street;
            Number = number;
            Complement = complement;
            District = district;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public static Address Of(string street, string number, string? complement, string district, string city, string state, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new ArgumentException("Rua é obrigatória.");
            if (string.IsNullOrWhiteSpace(city)) throw new ArgumentException("Cidade é obrigatória.");
            if (string.IsNullOrWhiteSpace(state)) throw new ArgumentException("Estado é obrigatório.");
            if (string.IsNullOrWhiteSpace(zipCode)) throw new ArgumentException("CEP é obrigatório.");

            return new Address(street, number, complement, district, city, state, zipCode);
        }
    }
}
