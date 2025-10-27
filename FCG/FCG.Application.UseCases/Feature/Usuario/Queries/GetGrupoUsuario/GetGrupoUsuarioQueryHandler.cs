using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetGrupoUsuario
{
    public class GetGrupoUsuarioQueryHandler : IRequestHandler<GetGrupoUsuarioQuery, GrupoUsuarioDto>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public GetGrupoUsuarioQueryHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<GrupoUsuarioDto> Handle(GetGrupoUsuarioQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var grupoUsuario  = await _grupoUsuarioRepository.GetByIdAsync(request.Id);
                if (grupoUsuario is null)
                {
                    throw new ArgumentException("Grupo de usuário não encontrado.");
                }

                // Dto
                return new GrupoUsuarioDto { Id = grupoUsuario.Id, Nome = grupoUsuario.Nome };
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
