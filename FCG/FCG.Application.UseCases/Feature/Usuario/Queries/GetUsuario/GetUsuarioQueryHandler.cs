using MediatR;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Dto.Usuario;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetUsuario
{
    public class GetUsuarioQueryHandler : IRequestHandler<GetUsuarioQuery, UsuarioDto>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetUsuarioQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioDto> Handle(GetUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuario = await _usuarioRepository.GetUsuarioAsync(request.Id);
            if (usuario is null)
            {
                throw new ArgumentException("Usuário não encontrado.");
            }

            return new UsuarioDto { Id = usuario.Id, Email = usuario.Email.Endereco, Nome = usuario.Nome, GrupoUsuarioId = usuario.GrupoUsuarioId };
        }
    }
}
