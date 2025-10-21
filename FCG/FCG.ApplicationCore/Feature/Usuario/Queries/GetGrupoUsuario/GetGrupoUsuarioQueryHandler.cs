// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.GetGrupoUsuario
{
    public class GetGrupoUsuarioQueryHandler : IRequestHandler<GetGrupoUsuarioQuery, GrupoUsuarioResponse>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public GetGrupoUsuarioQueryHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<GrupoUsuarioResponse> Handle(GetGrupoUsuarioQuery request, CancellationToken cancellationToken)
        {
            var grupoUsuario = await _grupoUsuarioRepository.GetByIdAsync(request.Id);
          
            if (grupoUsuario is null)
            {
                throw new ArgumentException("Grupo de usuário não encontrado.");
            }

            return new GrupoUsuarioResponse { Id = grupoUsuario.Id, Nome = grupoUsuario.Nome };
        }

    }
}
