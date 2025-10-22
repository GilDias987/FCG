using FluentValidation;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.AddUsuario
{
    public sealed class AddUsuarioValidator : AbstractValidator<AddUsuarioCommand>
    {
        public AddUsuarioValidator()
        {
            RuleFor(c => c.Nome).NotEmpty().WithMessage("Informe o nome.");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Informe o e-mail.");
            RuleFor(c => c.Senha).NotEmpty().WithMessage("Informe a senha.");
            RuleFor(c => c.UsuarioGrupoId).NotEmpty().WithMessage("Informe o grupo de usuário.");
        }
    }
}
