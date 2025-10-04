using Common.ResultPattern;

namespace PropostaFacil.Application.Tenants;

public static class TenantErrors
{
    public static Error NotFound(Guid id) =>
        Error.NotFound("Tenants.NotFound", $"Tenant with Id: {id} not found");

    public static Error Conflict(string document) =>
        Error.Conflict("Tenants.Conflict", $"Tenant with document: {document} already exists");

    public static Error TenantRequired() =>
        Error.Validation("Tenants.Validation", $"TenantId is required.");

    public static Error CreateFailure =>
        Error.Failure("Tenants.CreateFailure", $"Something went wrong in creating tenant");

    public static Error UpdateFailure =>
        Error.Failure("Tenants.UpdateFailure", $"Something went wrong in updating tenant");

    public static Error DeleteFailure =>
        Error.Failure("Tenants.DeleteFailure", $"Something went wrong in deleting tenant");
}
