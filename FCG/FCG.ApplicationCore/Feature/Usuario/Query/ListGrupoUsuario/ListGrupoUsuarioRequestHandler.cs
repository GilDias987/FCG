using FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario;
using FCG.ApplicationCore.Feature.Usuario.Query.LIstGrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Query.ListGrupoUsuario
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
