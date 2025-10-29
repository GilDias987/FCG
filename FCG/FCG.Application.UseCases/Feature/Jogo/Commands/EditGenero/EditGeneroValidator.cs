using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.EditGenero
{
    public sealed class EditGeneroValidator : AbstractValidator<EditGeneroCommand>
    {
        private readonly IGeneroRepository _generoRepository;
        public EditGeneroValidator(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;

            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o titulo do genero.");

            RuleFor(x => x.Id)
            .MustAsync(async (Id, cancellation) => (await generoRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
            .WithMessage("O id do genero não foi encontrado.");

            RuleFor(x => x.Titulo)
              .NotEmpty()
              .WithMessage("Informe o titulo do genero.")
              .MustAsync(async (model, context, cancellationToken) =>
              {
                   var genero = await _generoRepository.GetByIdAsync(model.Id);

                  if (genero != null && genero.Titulo != model.Titulo)
                  {
                      var verificaGenero = await _generoRepository.ExistsByAsync(x => x.Titulo == model.Titulo);
                      return !verificaGenero;
                  }

                  return true;
              })
              .WithMessage("Já existe um genero com esse título.");

        }
    }
}
