using Common.ResultPattern;

namespace PropostaFacil.Application.Clients
{
    public static class ClientErrors
    {
        public static Error NotFound(Guid id) =>
            Error.NotFound("Tenants.NotFound", $"Client with Id: {id} not found");

        public static Error Conflict(string document) =>
            Error.Conflict("Clients.Conflict", $"Client with Document: {document} already exists");

        public static Error CreateFailure =>
            Error.Failure("Clients.CreateFailure", $"Something went wrong in creating Client");

        public static Error UpdateFailure =>
            Error.Failure("Clients.UpdateFailure", $"Something went wrong in updating Client");

        public static Error DeleteFailure =>
            Error.Failure("Clients.DeleteFailure", $"Something went wrong in deleting Client");
    }
}
