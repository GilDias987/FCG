using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.AddPlataforma
{
    public sealed class AddPlataformaCommandValidator : AbstractValidator<AddPlataformaCommand>
    {
        public AddPlataformaCommandValidator()
        {
            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");
        }
    }
}
