using Common.ResultPattern;

namespace PropostaFacil.Application.Tenants
{
    public static class TenantErrors
    {
        public static Error NotFound(Guid id) =>
            Error.NotFound("Tenants.NotFound", $"Tenant with Id: {id} not found");

        public static Error Conflict(string name) =>
            Error.Conflict("Tenants.Conflict", $"Tenant with Name: {name} already exists");

        public static Error CreateFailure =>
            Error.Failure("Tenants.CreateFailure", $"Something went wrong in creating company");

        public static Error UpdateFailure =>
            Error.Failure("Tenants.UpdateFailure", $"Something went wrong in updating company");

        public static Error DeleteFailure =>
            Error.Failure("Tenants.DeleteFailure", $"Something went wrong in deleting company");
    }
}
