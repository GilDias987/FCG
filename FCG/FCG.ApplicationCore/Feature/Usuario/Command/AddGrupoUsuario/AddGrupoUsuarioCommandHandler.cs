using FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Command.AddGrupoUsuario
{
    public class AddGrupoUsuarioCommandHandler : IRequestHandler<AddGrupoUsuarioCommand, int>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public AddGrupoUsuarioCommandHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<int> Handle(AddGrupoUsuarioCommand request, CancellationToken cancellationToken)
        {
            var grupo = new GrupoUsuario(request.Nome);
            var retGrupo = await _grupoUsuarioRepository.AddAsync(grupo);
            return retGrupo.Id;
        }

    }
}
