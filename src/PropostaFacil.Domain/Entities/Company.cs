namespace PropostaFacil.Domain.Entities
{
    public class Company
    {
        public Company(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Guid Id { get; private set; } = default!;

        public string Name { get; private set; } = default!;
    }
}
