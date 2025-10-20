namespace PropostaFacil.Domain.ValueObjects;

public record Document
{
    public string Number { get; init; }

    private Document(string number)
    {
        Number = number;
    }

    public static Document Of(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Documento é obrigatório.");

        return new Document(number);
    }
}
