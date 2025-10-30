using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.ValueObjects;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.AddUsuario
{
    public class AddUsuarioCommandHandler : IRequestHandler<AddUsuarioCommand, UsuarioDto>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IGrupoUsuarioRepository _UsuarioGrupoRepository;

        public AddUsuarioCommandHandler(IUsuarioRepository usuarioRepository, IGrupoUsuarioRepository usuarioGrupoRepository)
        {
            _usuarioRepository      = usuarioRepository;
            _UsuarioGrupoRepository = usuarioGrupoRepository;
        }

        /// <summary>
        /// Adicionar usuário
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<UsuarioDto>Handle(AddUsuarioCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var objUsuario = await _usuarioRepository.AddAsync(new Domain.Entities.Usuario(request.Nome, new Email( request.Email),new Senha(request.Senha), request.UsuarioGrupoId));

                return new UsuarioDto() { Id = objUsuario.Id, Nome = objUsuario.Nome, Email = objUsuario.Email.Endereco, GrupoUsuarioId = objUsuario.GrupoUsuarioId };
            }
            catch (Exception)
            {
                throw new Exception("Ao Adicionar o usuário ocorreu uma falha inesperada. Tente novamente mais tarde.");
            }
        }
    }
}
