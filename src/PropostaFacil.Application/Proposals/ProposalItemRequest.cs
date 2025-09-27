namespace PropostaFacil.Application.Proposals;

public record ProposalItemRequest(string Name, string Description, int Quantity, decimal UnitPrice);
