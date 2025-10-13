using FCG.ApplicationCore.Dto.Autenticacao.Usuario;
using FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario;
using FCG.ApplicationCore.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Query.LoginUsuario
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
            var exceptionMessage = new ArgumentException("E-mail ou senha inválidos.");

            var usuarioEmail = await _usuarioRepository.UsuarioEmailAsync(request.Email.Trim());

            var usuarioDomionio = usuarioEmail?.Email.ObterDominio();

            if (usuarioEmail == null)
                throw exceptionMessage;

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
