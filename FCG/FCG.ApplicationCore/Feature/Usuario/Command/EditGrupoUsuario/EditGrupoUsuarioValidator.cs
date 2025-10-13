using FCG.ApplicationCore.Feature.Usuario.Command.AddGrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Command.EditGrupoUsuario
{
    public sealed class EditGrupoUsuarioValidator : AbstractValidator<EditGrupoUsuarioCommand>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        public EditGrupoUsuarioValidator(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;

            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Informe o id do grupo de usuario.")
              .GreaterThan(0)
              .WithMessage("O id deve ser maior que zero.")
              .MustAsync(async (Id, cancellation) => (await _grupoUsuarioRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id informado não foi encontrado.");

            RuleFor(x => x.Nome)
              .NotEmpty()
              .WithMessage("Informe o nome do grupo.")
              .MustAsync(async (model, context, cancellationToken) =>
              {
                  var grupousuario = await _grupoUsuarioRepository.GetByIdAsync(model.Id);

                  if(grupousuario != null && grupousuario.Nome != model.Nome)
                  {
                      var verificaGrupoUsuario = await _grupoUsuarioRepository.VerificarSeExisteGrupoAsync(model.Nome);
                      return !verificaGrupoUsuario;
                  }

                  return true;
              })
              .WithMessage("Já existe um grupo de usuário com esse nome.");

        }
    }
}
