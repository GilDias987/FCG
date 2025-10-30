using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.EditPlataforma
{
    public sealed class EditPlataformaValidator : AbstractValidator<EditPlataformaCommand>
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public EditPlataformaValidator(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;

            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o titulo da plataforma.");

            RuleFor(x => x.Id)
            .MustAsync(async (Id, cancellation) => (await plataformaRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
            .WithMessage("O id da plataforma não foi encontrado.");

            RuleFor(x => x.Titulo)
              .NotEmpty()
              .WithMessage("Informe o titulo da plataforma.")
              .MustAsync(async (model, context, cancellationToken) =>
              {
                  var genero = await plataformaRepository.GetByIdAsync(model.Id);
                  if (genero != null && genero.Titulo != model.Titulo)
                  {
                      var verificaGenero = await plataformaRepository.ExistsByAsync(x => x.Titulo == model.Titulo);
                      return !verificaGenero;
                  }

                  return true;
              })
              .WithMessage("Já existe uma plataforma com esse título.");
        }
    }
}
