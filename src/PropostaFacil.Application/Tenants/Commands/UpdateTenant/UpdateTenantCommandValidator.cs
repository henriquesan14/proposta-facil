using FluentValidation;
using PropostaFacil.Application.Tenants.Commands.UpdateTenant;

namespace PropostaFacil.Application.Tenants.Commands.CreateTenant;

public class UpdateTenantCommandValidator : AbstractValidator<UpdateTenantCommand>
{
    public UpdateTenantCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");
        RuleFor(c => c.Name).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .MaximumLength(100).WithMessage("O campo {PropertyName} não pode ter mais de 1000 caracteres");

        RuleFor(c => c.Document).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .MaximumLength(14).WithMessage("O campo {PropertyName} não pode ter mais de 14 caracteres");

        RuleFor(c => c.Domain).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .MaximumLength(300).WithMessage("O campo {PropertyName} não pode ter mais de 300 caracteres");
    }
}
