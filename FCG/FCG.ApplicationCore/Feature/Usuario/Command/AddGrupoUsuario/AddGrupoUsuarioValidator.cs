using FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Command.AddGrupoUsuario
{
    public sealed class AddGrupoUsuarioValidator : AbstractValidator<AddGrupoUsuarioCommand>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        public AddGrupoUsuarioValidator(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;

            RuleFor(x => x.Nome)
              .NotEmpty()
              .WithMessage("Informe o nome do grupo.")
              .MustAsync(async (Nome, cancellation) => string.IsNullOrEmpty(Nome) ? true : !(await _grupoUsuarioRepository.VerificarSeExisteGrupoAsync(Nome))) // Chame seu método aqui
              .WithMessage("Já existe um grupo de usuário com esse nome.");

        }
    }
}
