using FluentValidation;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.AddPlataforma
{
    public sealed class AddPlataformaCommandValidator : AbstractValidator<AddPlataformaCommand>
    {
        public AddPlataformaCommandValidator()
        {
            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");
        }
    }
}
