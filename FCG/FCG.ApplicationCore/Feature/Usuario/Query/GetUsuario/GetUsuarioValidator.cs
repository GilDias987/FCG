using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario
{
    public sealed class GetUsuarioValidator
    : AbstractValidator<GetUsuarioRequest>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public GetUsuarioValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id do usuário.")
              .GreaterThan(0)
              .WithMessage("O id deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _usuarioRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id informado não foi encontrado.");
        }
    }
}
