using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.DeleteUsuario
{
    public class DeleteUsuarioCommandHandler : IRequestHandler<DeleteUsuarioCommand, bool>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public DeleteUsuarioCommandHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool>Handle(DeleteUsuarioCommand request, CancellationToken cancellationToken)
        {
            var repUsuario  = await _usuarioRepository.GetByIdAsync(request.Id);
            if (repUsuario != null)
            {
                await _usuarioRepository.DeleteAsync(repUsuario.Id);

                return true;
            }
            else
            {
                return false;

                throw new ArgumentException("Usuário não foi encontrado.");
            }
        }
    }
}
