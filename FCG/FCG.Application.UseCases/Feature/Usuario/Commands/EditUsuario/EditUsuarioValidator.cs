using FCG.ApplicationCore.Interface.Repository;
using FluentValidation;
using System.Text.RegularExpressions;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.EditUsuario
{

    public sealed class EditUsuarioValidator : AbstractValidator<EditUsuarioCommand>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public EditUsuarioValidator(IGrupoUsuarioRepository grupoUsuarioRepository, IUsuarioRepository usuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
            _usuarioRepository = usuarioRepository;

            RuleFor(x => x.Id)
              .MustAsync(async (Id, cancellation) => (await _usuarioRepository.GetByIdAsync(Id)) != null ? true : false) // Chame seu método aqui
              .WithMessage("O id do usuário informado não foi encontrado.");

            RuleFor(c => c.Nome).NotEmpty().WithMessage("Informe o nome.");
            
            RuleFor(c => c.Senha)
                 .NotEmpty()
                 .WithMessage("Informe a senha.")
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
          
            RuleFor(c => c.UsuarioGrupoId).NotEmpty().WithMessage("Informe o grupo do usuário.");

            RuleFor(x => x.UsuarioGrupoId)
              .MustAsync(async (UsuarioGrupoId, cancellation) => (await _grupoUsuarioRepository.GetByIdAsync(UsuarioGrupoId)) != null ? true : false) 
              .WithMessage("O id de grupo de usuário não foi encontrado.");

            RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Informe um e-mail válido.")
            .MustAsync(async (model, context, cancellationToken) =>
            {
                var usuario = await _usuarioRepository.GetByIdAsync(model.Id);

                if (usuario != null && usuario.Email.Endereco != model.Email)
                {
                    var verificaUsuario = await _usuarioRepository.VerificarSeExisteUsuarioEmailAsync(model.Nome);
                    return !verificaUsuario;
                }

                return true;
            })
            .WithMessage("Este e-mail já está registrado. Por favor, tente outro.");
        }
    }
}
