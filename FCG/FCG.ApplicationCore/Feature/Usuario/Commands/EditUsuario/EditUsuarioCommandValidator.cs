using FluentValidation;

namespace FCG.ApplicationCore.Feature.Usuario.Commands.EditUsuario
{
    public sealed class EditUsuarioCommandValidator : AbstractValidator<EditUsuarioCommand>
    {
        public EditUsuarioCommandValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Informe o nome.");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Informe o e-mail.");
            RuleFor(c => c.Senha).NotEmpty().WithMessage("Informe a senha.");
            RuleFor(c => c.UsuarioGrupoId).NotEmpty().WithMessage("Informe o grupo do usuário.");
        }
    }
}
