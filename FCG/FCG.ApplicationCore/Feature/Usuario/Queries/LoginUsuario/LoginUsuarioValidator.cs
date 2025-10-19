using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.LoginUsuario
{
    public sealed class LoginUsuarioValidator : AbstractValidator<LoginUsuarioRequest>
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

            //RuleFor(x => x.Email)
            //   .MustAsync(async (Email, cancellation) => {

            //       string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            //       bool valida = Regex.IsMatch(Email, pattern, RegexOptions.IgnoreCase);

            //       if (valida)
            //       {
            //           var usuarioEmail = await _usuarioRepository.VerificarSeExisteUsuarioEmailAsync(Email);
            //           return usuarioEmail;
            //       }

            //       return true;
                    
            //   })
            //   .WithMessage("E-mail não cadastrado na plataforma FCG.");

            RuleFor(x => x.Senha)
              .NotEmpty()
              .WithMessage("Informe o senha.");

            //RuleFor(x => x.Senha)
            //   .MustAsync(async (Senha, cancellation) => {

            //       if (Senha.Length < 8 || !Regex.IsMatch(Senha, "[a-zA-Z]") || !Regex.IsMatch(Senha, "[0-9]") || !Regex.IsMatch(Senha, "[^a-zA-Z0-9]"))
            //           return false;

            //       return true;

            //   })
            //  .When(x => !string.IsNullOrEmpty(x.Senha))
            //  .WithMessage("A senha deve conter mais de 8 caracteres, incluindo pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");
  
        }
    }
}
