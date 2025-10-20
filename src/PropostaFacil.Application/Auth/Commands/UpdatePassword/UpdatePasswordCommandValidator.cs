using FluentValidation;

namespace PropostaFacil.Application.Auth.Commands.UpdatePassword;

public class UpdatePasswordCommandValidator : AbstractValidator<UpdatePasswordCommand>
{
    public UpdatePasswordCommandValidator()
    {
        RuleFor(x => x.OldPassword)
            .NotEmpty().WithMessage("A senha antiga é obrigatória.");

        RuleFor(x => x.NewPassword)
            .NotEmpty().WithMessage("A nova senha é obrigatória.")
            .MinimumLength(8).WithMessage("A senha deve ter pelo menos 8 caracteres.")
            .Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.")
            .Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.")
            .Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número.")
            .Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caractere especial.");
    }
}
