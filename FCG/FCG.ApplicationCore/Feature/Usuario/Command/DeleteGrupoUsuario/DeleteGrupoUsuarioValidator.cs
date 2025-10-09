using FCG.ApplicationCore.Feature.Usuario.Command.AddGrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Command.DeleteGrupoUsuario
{
    public sealed class DeleteGrupoUsuarioValidator : AbstractValidator<DeleteGrupoUsuarioCommand>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        public DeleteGrupoUsuarioValidator(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;

            RuleFor(x => x.Id)
                 .NotEmpty()
                 .WithMessage("Informe o id do usuário.")
                 .GreaterThan(0)
                 .WithMessage("O id deve ser maior que zero.")
                 .MustAsync(async (Id, cancellation) => (await _grupoUsuarioRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
                 .WithMessage("O id informado não foi encontrado.");

        }
    }
}
