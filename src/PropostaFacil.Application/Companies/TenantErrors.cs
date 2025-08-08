using Common.ResultPattern;

namespace PropostaFacil.Application.Companies
{
    public static class TenantErrors
    {
        public static Error NotFound(string id) =>
            Error.NotFound("Companies.NotFound", $"Company with Id: {id} not found");

        public static Error Conflict(string name) =>
            Error.Conflict("Companies.Conflict", $"Company with Name: {name} already exists");

        public static Error CreateFailure =>
            Error.Failure("Companies.CreateFailure", $"Something went wrong in creating company");

        public static Error UpdateFailure =>
            Error.Failure("Companies.UpdateFailure", $"Something went wrong in updating company");

        public static Error DeleteFailure =>
            Error.Failure("Companies.DeleteFailure", $"Something went wrong in deleting company");
    }
}
