using Common.ResultPattern;

namespace PropostaFacil.Application.Users
{
    public class UserErrors
    {
        public static Error NotFound(Guid id) =>
            Error.NotFound("Users.NotFound", $"User with Id: {id} not found");

        public static Error Conflict(string email) =>
            Error.Conflict("Users.Conflict", $"User with email: {email} already exists");

        public static Error Forbidden() =>
            Error.AccessForbidden("Users.Forbidden", $"This user does not have permission to do this.");

        public static Error TenantRequired() =>
            Error.Validation("Users.Validation", $"TenantId is required.");
    }
}
