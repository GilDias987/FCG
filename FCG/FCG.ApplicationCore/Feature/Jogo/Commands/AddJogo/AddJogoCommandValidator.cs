using FluentValidation;

namespace FCG.ApplicationCore.Feature.Jogo.Commands.AddJogo
{
    public sealed class AddJogoCommandValidator : AbstractValidator<AddJogoCommand>
    {
        public AddJogoCommandValidator()
        {
            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");
            RuleFor(c => c.Preco).NotEmpty().WithMessage("Informe o preço.");
            RuleFor(c => c.Desconto).NotEmpty().WithMessage("Informe o desconto.");
            RuleFor(c => c.GeneroId).NotEmpty().WithMessage("Informe o gênero do jogo.");
            RuleFor(c => c.PlataformaId).NotEmpty().WithMessage("Informe a plataforma do jogo.");
        }
    }
}
