using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.EditGrupoUsuario
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
