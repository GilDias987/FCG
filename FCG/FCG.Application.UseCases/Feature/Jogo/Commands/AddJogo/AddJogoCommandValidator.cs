using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.AddJogo
{
    public sealed class AddJogoCommandValidator : AbstractValidator<AddJogoCommand>
    {
        private readonly IGeneroRepository _generoRepository;
        private readonly IPlataformaRepository _plataformaRepository;
        public AddJogoCommandValidator(IGeneroRepository generoRepository, IPlataformaRepository plataformaRepository)
        {
            _generoRepository = generoRepository;
            _plataformaRepository = plataformaRepository;

            RuleFor(c => c.Titulo).NotEmpty().WithMessage("Informe o título.");

            RuleFor(c => c.Preco).NotEmpty()
                .WithMessage("Informe o preço.")
                .Must((model, context) =>
                {
                   if (model.Desconto.HasValue)
                   {
                       return !(model.Desconto.Value != Math.Round(model.Desconto.Value, 2));
                   }

                   return true;
                })
                .WithMessage("O preço não pode conter mais de duas casas decimais");

            RuleFor(x => x.GeneroId)
              .MustAsync(async (GeneroId, cancellation) => (await _generoRepository.GetByIdAsync(GeneroId)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id do genero do jogo não foi encontrado.");

            RuleFor(x => x.PlataformaId)
               .MustAsync(async (PlataformaId, cancellation) => (await _plataformaRepository.GetByIdAsync(PlataformaId)) != null ? true : false) // Chame seu método aqui
               .WithMessage("O id do plataforma do jogo não foi encontrado.");

            RuleFor(x => x.Desconto)
                .Must((model, context) =>
                {
                    if (model.Desconto.HasValue)
                    {
                        if (model.Desconto < 0 || model.Desconto > 100)
                            return false;
                    }

                    return true;
                })
                .WithMessage("O percentual do desconto não pode ser negativo ou maior que 100.")
                   .Must((model, context) =>
                   {
                       if (model.Desconto.HasValue)
                       {
                           return !(model.Desconto.Value != Math.Round(model.Desconto.Value, 2));
                       }

                       return true;
                   })
                .WithMessage("O percentual do desconto não pode conter mais de duas casas decimais");
        }
    }
}
