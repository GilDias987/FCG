using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Dto.Jogo;
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

        public async Task<UsuarioDto>Handle(EditUsuarioCommand request, CancellationToken cancellationToken)
        {
            var objUsuario  = await _usuarioRepository.GetByIdAsync(request.Id);
            if (objUsuario != null)
            {
                var usuExiste = await _usuarioRepository.GetByEmailExistsAsync(request.Id, request.Email);
                if (usuExiste is true)
                    throw new ArgumentException("Este e-mail já está registrado. Por favor, tente outro.");

                var gruExiste   = await _UsuarioGrupoRepository.GetByIdExistsAsync(request.UsuarioGrupoId);
                if (gruExiste is false)
                    throw new ArgumentException("O Grupo de usuário não foi encontrado.");

                objUsuario.Inicializar(request.Nome, new Email(request.Email), new Senha(request.Senha), request.UsuarioGrupoId);

                await _usuarioRepository.UpdateAsync(objUsuario);

                return new UsuarioDto() { Id = objUsuario.Id, Nome = objUsuario.Nome, Email = objUsuario.Email.Endereco, GrupoUsuarioId = objUsuario.GrupoUsuarioId };
            }
            else
            {
                throw new ArgumentException("Usuário não foi encontrado.");
            }
        }
    }
}
