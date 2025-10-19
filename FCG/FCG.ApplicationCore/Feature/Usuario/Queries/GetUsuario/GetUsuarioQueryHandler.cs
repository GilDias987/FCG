using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.GetUsuario
{
    public class GetUsuarioQueryHandler : IRequestHandler<GetUsuarioQuery, GetUsuarioResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuarioQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<GetUsuarioResponse> Handle(GetUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetUsuarioAsync(request.Id);
            if (usuario is null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            return new GetUsuarioResponse { Id = usuario.Id, Email = usuario.Email, Nome = usuario.Nome, Grupo = usuario.GrupoUsuario.Nome };
        }
    }
}
