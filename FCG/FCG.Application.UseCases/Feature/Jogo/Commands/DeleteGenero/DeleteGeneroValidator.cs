using FCG.Application.UseCases.Feature.Jogo.Commands.DeleteJogo;
using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.DeleteGenero
{
    public sealed class DeleteGeneroValidator : AbstractValidator<DeleteGeneroCommand>
    {
        private readonly IGeneroRepository _generoRepository;
        public DeleteGeneroValidator(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id do genero.")
              .GreaterThan(0)
              .WithMessage("O id do genero deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _generoRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id do genero informado não foi encontrado.");

        }
    }
}
