using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.GetGrupoUsuario
{
    public class GetGrupoUsuarioRequestHandler : IRequestHandler<GetGrupoUsuarioRequest, GrupoUsuarioResponse>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public GetGrupoUsuarioRequestHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<GrupoUsuarioResponse> Handle(GetGrupoUsuarioRequest request, CancellationToken cancellationToken)
        {
            var grupoUsuario = await _grupoUsuarioRepository.GetByIdAsync(request.Id);
            return new GrupoUsuarioResponse { Id = grupoUsuario.Id, Nome = grupoUsuario.Nome };
        }

    }
}
