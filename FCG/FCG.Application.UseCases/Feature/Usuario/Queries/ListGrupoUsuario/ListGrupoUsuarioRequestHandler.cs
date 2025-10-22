using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Application.UseCases.Feature.Usuario.Queries;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.ListGrupoUsuario
{
    public class ListGrupoUsuarioRequestHandler : IRequestHandler<ListGrupoUsuarioRequest, List<GrupoUsuarioResponse>>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public ListGrupoUsuarioRequestHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<List<GrupoUsuarioResponse>> Handle(ListGrupoUsuarioRequest request, CancellationToken cancellationToken)
        {
            var lstGrupoUsuario = await _grupoUsuarioRepository.ListarGrupoUsuario();
            
            return lstGrupoUsuario.Select(g => new GrupoUsuarioResponse
            {
                Id = g.Id,
                Nome = g.Nome
            }).ToList();
        }

    }
}
