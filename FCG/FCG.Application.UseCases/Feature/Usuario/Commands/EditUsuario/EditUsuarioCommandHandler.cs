using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.ValueObjects;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.EditUsuario
{
    public class EditUsuarioCommandHandler : IRequestHandler<EditUsuarioCommand, UsuarioDto>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGrupoUsuarioRepository _UsuarioGrupoRepository;

        public EditUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IGrupoUsuarioRepository usuarioGrupoRepository)
        {
            _usuarioRepository      = usuarioRepository;
            _UsuarioGrupoRepository = usuarioGrupoRepository;
        }

        public async Task<UsuarioDto> Handle(EditUsuarioCommand request, CancellationToken cancellationToken)
        {


            try
            {
                var objUsuario = await _usuarioRepository.GetByIdAsync(request.Id);
                objUsuario.Inicializar(request.Nome, new Email(request.Email), new Senha(request.Senha), request.UsuarioGrupoId);
                await _usuarioRepository.UpdateAsync(objUsuario);

                return new UsuarioDto() { Id = objUsuario.Id, Nome = objUsuario.Nome, Email = objUsuario.Email.Endereco, GrupoUsuarioId = objUsuario.GrupoUsuarioId };
            }
            catch (Exception)
            {
                throw new Exception("Ao Editar o usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
