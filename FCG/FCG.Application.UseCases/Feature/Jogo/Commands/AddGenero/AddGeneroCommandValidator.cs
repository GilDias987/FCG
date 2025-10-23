using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.AddGenero
{
    public sealed class AddGeneroCommandValidator : AbstractValidator<AddGeneroCommand>
    {
        public AddGeneroCommandValidator()
        {
            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");
        }
    }
}
