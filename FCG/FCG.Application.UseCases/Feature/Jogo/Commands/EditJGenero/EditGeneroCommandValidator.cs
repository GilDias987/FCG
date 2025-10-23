using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.EditJGenero
{
    public sealed class EditGeneroCommandValidator : AbstractValidator<EditGeneroCommand>
    {
        public EditGeneroCommandValidator()
        {
            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");
        }
    }
}
