// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Common.Exceptions;
using FluentValidation;
using System;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.EditGrupoUsuario
{
    public sealed class EditGrupoUsuarioValidator : AbstractValidator<EditGrupoUsuarioCommand>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        public EditGrupoUsuarioValidator(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;

            RuleFor(x => x.Id)
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
