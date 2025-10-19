using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;

namespace FCG.ApplicationCore.Feature.Usuario.Commands.AddGrupoUsuario
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
