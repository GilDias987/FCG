using FluentValidation;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.EditPlataforma
{
    public sealed class EditPlataformaCommandValidator : AbstractValidator<EditPlataformaCommand>
    {
        public EditPlataformaCommandValidator()
        {
            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");
        }
    }
}
