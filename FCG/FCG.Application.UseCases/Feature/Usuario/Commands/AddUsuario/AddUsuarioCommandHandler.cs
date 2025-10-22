using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Dto.Jogo;
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
            // Usando FluentValidation
            var repUsuario  = await _usuarioRepository.UsuarioEmailAsync(request.Email);
            if (repUsuario != null)
                throw new ArgumentException("Este e-mail já está registrado. Por favor, tente outro.");

            var repUsuarioGrupo = await _UsuarioGrupoRepository.GetByIdExistsAsync(request.UsuarioGrupoId);
            if (repUsuarioGrupo is false)
                throw new ArgumentException("O Grupo de usuário não foi encontrado.");

            // Usando ServiceResponse
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
