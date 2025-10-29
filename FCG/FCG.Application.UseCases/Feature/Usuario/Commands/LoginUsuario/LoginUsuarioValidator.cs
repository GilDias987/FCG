using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.LoginUsuario
{
    public sealed class LoginUsuarioValidator : AbstractValidator<LoginUsuarioCommand>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public LoginUsuarioValidator(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.Email)
              .NotEmpty()
              .WithMessage("Informe o e-mail");

            RuleFor(x => x.Email)
               .EmailAddress()
               .When(x => !string.IsNullOrEmpty(x.Email))
               .WithMessage("E-mail inválido.");

            RuleFor(x => x.Senha)
              .NotEmpty()
              .WithMessage("Informe o senha.");

        }
    }
}
