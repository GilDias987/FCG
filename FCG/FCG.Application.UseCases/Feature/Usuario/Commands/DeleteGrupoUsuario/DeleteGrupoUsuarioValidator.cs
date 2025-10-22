using FluentValidation;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.DeleteGrupoUsuario
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
