using Common.ResultPattern;

namespace PropostaFacil.Application.Proposals;

public static class ProposalErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Proposals.NotFound", $"Tenant with Id: {id} not found");

    public static Error Conflict(string document) =>
        Error.Conflict("Proposals.Conflict", $"Proposal with document: {document} already exists");

    public static Error Forbidden(Guid clientId) =>
        Error.AccessForbidden("Proposals.Forbidden", $"The specified client {clientId.ToString()} does not belong to the given tenant.");

    public static Error CreateFailure =>
        Error.Failure("Proposals.CreateFailure", $"Something went wrong in creating company");

    public static Error UpdateFailure =>
        Error.Failure("Proposals.UpdateFailure", $"Something went wrong in updating company");

    public static Error DeleteFailure =>
        Error.Failure("Proposals.DeleteFailure", $"Something went wrong in deleting company");
}
