using FCG.Application.UseCases.Feature.Jogo.Commands.DeletePlataforma;
using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.DeleteGenero
{
    public sealed class DeletePlataformaCommandValidator : AbstractValidator<DeletePlataformaCommand>
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public DeletePlataformaCommandValidator(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id da plataforma.")
              .GreaterThan(0)
              .WithMessage("O id da plataforma deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _plataformaRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id da plataforma informado não foi encontrado.");
        }
    }
}
