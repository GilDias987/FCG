using FCG.Application.UseCases.Feature.Usuario.Commands.AddGrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Application.UseCases.Feature.Jogo.Commands.VincularDescontoJogo
{
    public class VincularDescontoJogoValidator : AbstractValidator<VincularDescontoJogoCommand>
    {
        private readonly IJogoRepository _jogoRepository;
        public VincularDescontoJogoValidator(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id do jogo.")
              .GreaterThan(0)
              .WithMessage("O id deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _jogoRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id do jogo informado não foi encontrado.");
          
            RuleFor(x => x.Desconto)
                      .Must((model, context) =>
                      {
                          if(model.Desconto.HasValue)
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
                      .WithMessage("O percentual do desconto não pode conter mais de duas casas decimais.");
        }
    }
}
