using Common.ResultPattern;

namespace PropostaFacil.Application.Users
{
    public class UserErrors
    {
        public static Error NotFound(Guid id) =>
            Error.NotFound("Users.NotFound", $"User with Id: {id} not found");

        public static Error Conflict(string email) =>
            Error.Conflict("Users.Conflict", $"User with email: {email} already exists");

        public static Error CreateFailure =>
            Error.Failure("Users.CreateFailure", $"Something went wrong in creating company");

        public static Error UpdateFailure =>
            Error.Failure("Users.UpdateFailure", $"Something went wrong in updating company");

        public static Error DeleteFailure =>
            Error.Failure("Users.DeleteFailure", $"Something went wrong in deleting company");
    }
}
