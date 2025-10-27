using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.AddGrupoUsuario
{
    public class AddGrupoUsuarioCommandHandler : IRequestHandler<AddGrupoUsuarioCommand, GrupoUsuarioDto>
    {
        private readonly IGrupoUsuarioRepository _grupoUsuarioRepository;

        public AddGrupoUsuarioCommandHandler(IGrupoUsuarioRepository grupoUsuarioRepository)
        {
            _grupoUsuarioRepository = grupoUsuarioRepository;
        }

        public async Task<GrupoUsuarioDto> Handle(AddGrupoUsuarioCommand request, CancellationToken cancellationToken)
        {

            try
            {
                var objGenero = await _grupoUsuarioRepository.AddAsync(new Domain.Entities.GrupoUsuario(request.Nome));

                return new GrupoUsuarioDto() { Id = objGenero.Id, Nome = objGenero.Nome };
            }
            catch (Exception)
            {
                throw new Exception("Ao cadastrar o Grupo de usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
