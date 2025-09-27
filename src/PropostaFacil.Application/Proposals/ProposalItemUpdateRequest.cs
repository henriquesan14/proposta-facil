namespace PropostaFacil.Application.Proposals;

public record ProposalItemUpdateRequest(Guid? Id, string Name, string Description, int Quantity, decimal UnitPrice);
