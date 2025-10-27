using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;
using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.LoginUsuario
{
    public class LoginUsuarioRequestHandler : IRequestHandler<LoginUsuarioRequest, UsuarioDto>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginUsuarioRequestHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioDto> Handle(LoginUsuarioRequest request, CancellationToken cancellationToken)
        {
            var usuarioEmail = await _usuarioRepository.UsuarioEmailAsync(request.Email.Trim());

            var senhaValida = usuarioEmail.ValidarSenha(request.Senha.Trim());

            if (!senhaValida)
                throw new ArgumentException("E-mail ou senha inválidos.");

            return new UsuarioDto { Id = usuarioEmail.Id, 
                                    Email = usuarioEmail.Email.Endereco, 
                                    Nome = usuarioEmail.Nome,
                                    Grupo = usuarioEmail.GrupoUsuario.Nome };
        }
    }
}
