using FluentValidation;

namespace PropostaFacil.Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
    {
        public CreateTenantCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(100).WithMessage("O campo {PropertyName} não pode ter mais de 1000 caracteres");

            RuleFor(c => c.Cnpj).NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .Length(14).WithMessage("O campo {PropertyName} tem que ter 14 caracteres");

            RuleFor(c => c.Domain).NotEmpty()
                .WithMessage("O campo {PropertyName} é obrigatório")
                .MaximumLength(300).WithMessage("O campo {PropertyName} não pode ter mais de 300 caracteres");
        }
    }
}
