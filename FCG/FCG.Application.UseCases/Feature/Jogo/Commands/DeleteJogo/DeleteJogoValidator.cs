using FCG.Application.UseCases.Feature.Jogo.Commands.AddJogo;
using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.DeleteJogo
{
    public sealed class DeleteJogoValidator : AbstractValidator<DeleteJogoCommand>
    {

        private readonly IJogoRepository _jogoRepository;
        public DeleteJogoValidator(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id do jogo.")
              .GreaterThan(0)
              .WithMessage("O id do jogo deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _jogoRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id do jogo informado não foi encontrado.");

        }
    }
}
