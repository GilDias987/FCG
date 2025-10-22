using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.EditJPlataforma
{
    public sealed class EditPlataformaCommandValidator : AbstractValidator<EditPlataformaCommand>
    {
        public EditPlataformaCommandValidator()
        {
            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");
        }
    }
}
