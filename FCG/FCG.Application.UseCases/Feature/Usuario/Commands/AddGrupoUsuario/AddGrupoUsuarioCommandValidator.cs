using FluentValidation;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.AddGrupoUsuario
{
    public sealed class AddGrupoUsuarioCommandValidator : AbstractValidator<AddGrupoUsuarioCommand>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        public AddGrupoUsuarioCommandValidator(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;

            RuleFor(x => x.Nome)
              .NotEmpty()
              .WithMessage("Informe o nome do grupo.")
              .MustAsync(async (Nome, cancellation) => string.IsNullOrEmpty(Nome) ? true : !(await _grupoUsuarioRepository.VerificarSeExisteGrupoAsync(Nome)))
              .WithMessage("Já existe um grupo de usuário com esse nome.");

        }
    }
}
