using FCG.ApplicationCore.Dto.Usuario;
// Dependências
using FCG.ApplicationCore.Interface.Repository;
using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.EditGrupoUsuario
{
    public class EditGrupoUsuarioCommandHandler : IRequestHandler<EditGrupoUsuarioCommand, GrupoUsuarioDto>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public EditGrupoUsuarioCommandHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<GrupoUsuarioDto> Handle(EditGrupoUsuarioCommand request, CancellationToken cancellationToken)
        {
            var grupo = await _grupoUsuarioRepository.GetByIdAsync(request.Id);
            grupo.Inicializar(request.Nome);
            await _grupoUsuarioRepository.UpdateAsync(grupo);
            return new GrupoUsuarioDto() { Id = grupo.Id, Nome = grupo.Nome };
        }
    }
}
