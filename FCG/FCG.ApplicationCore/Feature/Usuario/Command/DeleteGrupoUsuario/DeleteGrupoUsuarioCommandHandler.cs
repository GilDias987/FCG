using FCG.ApplicationCore.Feature.Usuario.Command.AddGrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Command.DeleteGrupoUsuario
{
    public class DeleteGrupoUsuarioCommandHandler : IRequestHandler<DeleteGrupoUsuarioCommand, int>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public DeleteGrupoUsuarioCommandHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<int> Handle(DeleteGrupoUsuarioCommand request, CancellationToken cancellationToken)
        {
            await _grupoUsuarioRepository.DeleteAsync(request.Id);
            return request.Id;
        }

    }
}
