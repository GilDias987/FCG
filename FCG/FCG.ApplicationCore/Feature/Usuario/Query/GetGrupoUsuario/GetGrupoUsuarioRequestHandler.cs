using FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario;
using FCG.ApplicationCore.Feature.Usuario.Query.LIstGrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Query.GetGrupoUsuario
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
