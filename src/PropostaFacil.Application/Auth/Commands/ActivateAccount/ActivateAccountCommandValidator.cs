﻿using FluentValidation;

namespace PropostaFacil.Application.Auth.Commands.ActivateAccount;

public class ActivateAccountCommandValidator : AbstractValidator<ActivateAccountCommand>
{
    public ActivateAccountCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("O token é obrigatório.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("A senha é obrigatória.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número.")
            .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caractere especial.");
    }
}
