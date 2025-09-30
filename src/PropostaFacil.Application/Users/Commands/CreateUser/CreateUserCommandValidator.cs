using FluentValidation;

namespace PropostaFacil.Application.Users.Commands.CreateUser;

public class AdminCreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public AdminCreateUserCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .MaximumLength(100).WithMessage("O campo {PropertyName} não pode ter mais de 1000 caracteres");

        RuleFor(c => c.Email)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .EmailAddress().WithMessage("O campo {PropertyName} tem que ser um email válido")
            .MaximumLength(200).WithMessage("O campo {PropertyName} não pode ter mais de 200 caracteres");

        RuleFor(c => c.PhoneNumber).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .Length(11).WithMessage("O campo {PropertyName} tem que ter 11 caracteres");

        RuleFor(c => c.Password).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .MinimumLength(6).WithMessage("O campo {PropertyName} não pode ter menos de 6 caracteres")
            .MaximumLength(72).WithMessage("O campo {PropertyName} não pode ter mais de 72 caracteres");

        RuleFor(c => c.Role)
            .IsInEnum().WithMessage("O campo {PropertyName} é inválido");
    }
}
