using FluentValidation;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.DeleteUsuario
{
    public sealed class DeleteUsuarioValidator : AbstractValidator<DeleteUsuarioCommand>
    {
        public DeleteUsuarioValidator()
        {
            RuleFor(c => c.Id).NotEmpty().WithMessage("Informe o id do usuário .");
        }
    }
}

