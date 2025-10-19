using FluentValidation;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.ApplicationCore.Feature.Usuario.Commands.AddGrupoUsuario
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
