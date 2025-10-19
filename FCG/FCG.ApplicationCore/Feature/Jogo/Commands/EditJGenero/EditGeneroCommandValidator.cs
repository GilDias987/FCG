using FluentValidation;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.EditGenero
{
    public sealed class EditGeneroCommandValidator : AbstractValidator<EditGeneroCommand>
    {
        public EditGeneroCommandValidator()
        {
            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");
        }
    }
}
