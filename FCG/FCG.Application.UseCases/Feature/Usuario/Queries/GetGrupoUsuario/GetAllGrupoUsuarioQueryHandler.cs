using MediatR;
using Microsoft.EntityFrameworkCore;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetGrupoUsuario
{
    public class GetAllGrupoUsuarioQueryHandler : IRequestHandler<GetAllGrupoUsuarioQuery, List<GrupoUsuarioDto>>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public GetAllGrupoUsuarioQueryHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<GrupoUsuarioDto>> Handle(GetAllGrupoUsuarioQuery request, CancellationToken cancellationToken)
        {
            var grupoUsuario = await _grupoUsuarioRepository.All.Select(s => new GrupoUsuarioDto
            {
                Id = s.Id, Nome = s.Nome
            }).ToListAsync();

            if (!grupoUsuario.Any())
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return grupoUsuario;
        }
    }
}
