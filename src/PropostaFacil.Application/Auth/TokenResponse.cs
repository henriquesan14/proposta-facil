﻿namespace PropostaFacil.Application.Auth
{
    public record TokenResponse(string AccessToken, string RefreshToken, DateTime AccessTokenExpiresAt, DateTime RefreshTokenExpiresAt);
}
