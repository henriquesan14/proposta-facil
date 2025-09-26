using FluentValidation;
using PropostaFacil.Application.Proposals;

namespace PropostaFacil.Application.Users.Commands.CreateUser;

public class ProposalItemRequestValidator : AbstractValidator<ProposalItemRequest>
{
    public ProposalItemRequestValidator()
    {
        RuleFor(i => i.Name)
            .NotEmpty()
            .WithMessage("O campo {PropertyName} do item é obrigatório")
            .MaximumLength(200).WithMessage("O campo {PropertyName} não pode ter mais de 1000 caracteres");

        RuleFor(i => i.Description)
            .NotEmpty()
            .WithMessage("O campo {PropertyName} do item é obrigatório")
            .MaximumLength(1000).WithMessage("O campo {PropertyName} não pode ter mais de 1000 caracteres");

        RuleFor(i => i.Quantity)
            .GreaterThan(0)
            .WithMessage("O campo {PropertyName} do item deve ser maior que zero");

        RuleFor(i => i.UnitPrice)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O campo {PropertyName} do item não pode ser menor que zero");
    }
}
