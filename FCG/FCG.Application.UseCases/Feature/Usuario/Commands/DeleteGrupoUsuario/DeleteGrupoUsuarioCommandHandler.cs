using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.DeleteGrupoUsuario
{
    public class DeleteGrupoUsuarioCommandHandler : IRequestHandler<DeleteGrupoUsuarioCommand, bool>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public DeleteGrupoUsuarioCommandHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<bool> Handle(DeleteGrupoUsuarioCommand request, CancellationToken cancellationToken)
        {
            var repGrupoUsuario  = await _grupoUsuarioRepository.GetByIdAsync(request.Id);
            if (repGrupoUsuario != null)
            {
                await _grupoUsuarioRepository.DeleteAsync(repGrupoUsuario.Id);

                return true;
            }
            else
            {
                return false;

                throw new ArgumentException("Grupo de usuário não foi encontrado.");
            }
        }
    }
}
