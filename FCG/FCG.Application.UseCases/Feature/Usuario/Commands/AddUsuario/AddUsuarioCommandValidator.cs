using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Common.Exceptions;
using FCG.Domain.ValueObjects;
using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.AddUsuario
{
    public sealed class AddUsuarioValidator : AbstractValidator<AddUsuarioCommand>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public AddUsuarioValidator(IGrupoUsuarioRepository grupoUsuarioRepository, IUsuarioRepository usuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
            _usuarioRepository = usuarioRepository;

            RuleFor(c => c.Nome).NotEmpty().WithMessage("Informe o nome.");

            RuleFor(c => c.Senha)
               .Must((model, context) =>
               {
                   if (model.Senha.Length < 8)
                   {
                       return false;
                   }
                   if (!Regex.IsMatch(model.Senha, "[a-zA-Z]"))
                   {
                       return false;
                   }
                   if (!Regex.IsMatch(model.Senha, "[0-9]"))
                   {
                       return false;
                   }
                   if (!Regex.IsMatch(model.Senha, "[0-9]"))
                   {
                       return false;
                   }

                   return true;
               })
               .WithMessage("A senha deve ter no mínimo 8 caracteres e incluir pelo menos uma letra maiúscula, um número e um caractere especial.");
            

            RuleFor(x => x.UsuarioGrupoId)
              .MustAsync(async (UsuarioGrupoId, cancellation) => (await _grupoUsuarioRepository.GetByIdAsync(UsuarioGrupoId)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id de grupo de usuário não foi encontrado.");

            RuleFor(c => c.Email)
                .EmailAddress()
                .WithMessage("Informe um e-mail válido.")
                .MustAsync(async (Email, cancellation) => !(await _usuarioRepository.VerificarSeExisteUsuarioEmailAsync(Email)) )
                .WithMessage("Este e-mail já está registrado. Por favor, tente outro."); ;

        }
    }
}
