using FCG.ApplicationCore.Interface.Repository;
using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.LoginUsuario
{
    public class LoginUsuarioRequestHandler : IRequestHandler<LoginUsuarioRequest, GetUsuarioResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginUsuarioRequestHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<GetUsuarioResponse> Handle(LoginUsuarioRequest request, CancellationToken cancellationToken)
        {
            var usuarioEmail = await _usuarioRepository.UsuarioEmailAsync(request.Email.Trim());

            var senhaValida = usuarioEmail.ValidarSenha(request.Senha.Trim());

            if (!senhaValida)
                throw exceptionMessage;

            return new GetUsuarioResponse { Id = usuarioEmail.Id, 
                                             Email = usuarioEmail.Email.Endereco, 
                                             Nome = usuarioEmail.Nome,
                                             Grupo = usuarioEmail.GrupoUsuario.Nome };
        }
    }
}
