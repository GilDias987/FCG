using FCG.ApplicationCore.Interface.Repository;
using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario
{
    public class GetEixoRequestQueryHandler : IRequestHandler<GetUsuarioRequest, GetUsuarioResponse>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetEixoRequestQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<GetUsuarioResponse> Handle(GetUsuarioRequest request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetUsuarioAsync(request.Id);
            if (usuario is null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            return new GetUsuarioResponse { Id = usuario.Id, Email = usuario.Email.Endereco, Nome = usuario.Nome, Grupo = usuario.GrupoUsuario.Nome };
        }
    }
}
