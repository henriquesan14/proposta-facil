namespace PropostaFacil.Application.Proposals;

public record ProposalItemResponse(Guid Id, string Name, string Description, int Quantity, decimal UnitPrice, decimal TotalPrice);
