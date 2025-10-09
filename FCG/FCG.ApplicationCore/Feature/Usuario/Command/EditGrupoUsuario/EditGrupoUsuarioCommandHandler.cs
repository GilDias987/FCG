using FCG.ApplicationCore.Feature.Usuario.Command.AddGrupoUsuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Command.EditGrupoUsuario
{
    public class EditGrupoUsuarioCommandHandler : IRequestHandler<EditGrupoUsuarioCommand, int>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public EditGrupoUsuarioCommandHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<int> Handle(EditGrupoUsuarioCommand request, CancellationToken cancellationToken)
        {
            var grupo = await _grupoUsuarioRepository.GetByIdAsync(request.Id);
            grupo.Inicializar(request.Nome);
            await _grupoUsuarioRepository.UpdateAsync(grupo);
            return grupo.Id;
        }

    }
}
