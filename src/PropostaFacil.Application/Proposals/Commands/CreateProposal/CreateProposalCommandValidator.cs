using FluentValidation;

namespace PropostaFacil.Application.Proposals.Commands.CreateProposal;

public class CreateProposalCommandValidator : AbstractValidator<CreateProposalCommand>
{
    public CreateProposalCommandValidator()
    {
        RuleFor(c => c.ClientId).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("O campo {PropertyName} é obrigatório")
            .MaximumLength(200).WithMessage("O campo {PropertyName} não pode ter mais de 200 caracteres");

        RuleFor(c => c.Currency).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .Length(3).WithMessage("O campo {PropertyName} tem que ter 3 caracteres");

        RuleFor(c => c.ValidUntil).NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .Must(date => date.Date >= DateTime.Today.Date)
            .WithMessage("O campo {PropertyName} não pode ser uma data passada");

        RuleFor(c => c.Items)
            .NotEmpty()
            .WithMessage("A proposta deve conter ao menos um item");

        RuleForEach(c => c.Items).SetValidator(new ProposalItemRequestValidator());
    }
}
