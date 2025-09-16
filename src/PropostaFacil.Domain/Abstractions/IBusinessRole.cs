namespace PropostaFacil.Domain.Abstractions;

public interface IBusinessRule
{
    string Message { get; }
    bool IsBroken();
}
