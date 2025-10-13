using FCG.ApplicationCore.Feature.Usuario.Command.EditGrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Query.LoginUsuario
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

            RuleFor(x => x.Senha)
              .NotEmpty()
              .WithMessage("Informe o senha.");

        }
    }
}
