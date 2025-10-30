using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.DeleteUsuario
{
    public sealed class DeleteUsuarioValidator : AbstractValidator<DeleteUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public DeleteUsuarioValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Informe o id do usuário.")
            .GreaterThan(0)
            .WithMessage("O id deve ser maior que zero.")
            .MustAsync(async (Id, cancellation) => (await _usuarioRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
            .WithMessage("O id do usuario informado não foi encontrado.");
        }
    }
}

