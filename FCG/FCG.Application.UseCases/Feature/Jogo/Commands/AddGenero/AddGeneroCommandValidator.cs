using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.AddGenero
{
    public sealed class AddGeneroCommandValidator : AbstractValidator<AddGeneroCommand>
    {
        private readonly IGeneroRepository _generoRepository;
        public AddGeneroCommandValidator(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;

            RuleFor(x => x.Titulo)
              .NotEmpty()
              .WithMessage("Informe o titulo do genero.")
              .MustAsync(async (Titulo, cancellation) => {
                  if (string.IsNullOrEmpty(Titulo))
                      return true;
                  else
                  {
                      var existeGenero = await _generoRepository.ExistsByAsync(x => x.Titulo.ToUpper() == Titulo.ToUpper());
                      return !existeGenero;
                  }
                }
              )
              .WithMessage("Já existe um gênero de jogo com esse título.");
        }
    }
}
