using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.AddPlataforma
{
    public sealed class AddPlataformaCommandValidator : AbstractValidator<AddPlataformaCommand>
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public AddPlataformaCommandValidator(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;

            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");

            RuleFor(x => x.Titulo)
              .NotEmpty()
              .WithMessage("Informe o titulo da plataforma.")
              .MustAsync(async (Titulo, cancellation) => {
                  if (string.IsNullOrEmpty(Titulo))
                      return true;
                  else
                  {
                      var existeGenero = await _plataformaRepository.ExistsByAsync(x => x.Titulo.ToUpper() == Titulo.ToUpper());
                      return !existeGenero;
                  }
                }
              )
              .WithMessage("Já existe uma plataforma com esse título.");
        }
    }
}
