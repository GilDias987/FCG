using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.DeleteGrupoUsuario
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
