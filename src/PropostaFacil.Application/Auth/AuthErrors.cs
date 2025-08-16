using Common.ResultPattern;

namespace PropostaFacil.Application.Auth
{
    public class AuthErrors
    {
        public static Error Unauthorized() =>
            Error.AccessUnAuthorized("Auth.Unauthorized", $"Email/Password incorrect");

    }
}
