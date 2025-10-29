using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;
using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.LoginUsuario
{
    public class LoginUsuarioCommandHandler : IRequestHandler<LoginUsuarioCommand, UsuarioDto>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioDto> Handle(LoginUsuarioCommand request, CancellationToken cancellationToken)
        {
            var argumentException = new ArgumentException("E-mail ou senha inválidos.");

            var usuarioEmail = await _usuarioRepository.UsuarioEmailAsync(request.Email.Trim());

            if (usuarioEmail is null)
                throw argumentException;

            var senhaValida = usuarioEmail.ValidarSenha(request.Senha.Trim());
             
            if (!senhaValida)
                throw argumentException;

            return new UsuarioDto { Id = usuarioEmail.Id, 
                                    Email = usuarioEmail.Email.Endereco, 
                                    Nome = usuarioEmail.Nome,
                                    Grupo = usuarioEmail.GrupoUsuario.Nome };
        }
    }
}
