using MediatR;
using Microsoft.EntityFrameworkCore;

// Dependências
using FCG.ApplicationCore.Interface.Repository;
using FCG.ApplicationCore.Dto.Usuario;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetAllUsuario
{
    public class GetAllUsuarioQueryHandler : IRequestHandler<GetAllUsuarioQuery, List<UsuarioDto>>
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public GetAllUsuarioQueryHandler(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        /// <summary>
        /// GetAllAsync
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<UsuarioDto>> Handle(GetAllUsuarioQuery request, CancellationToken cancellationToken)
        {
            var usuarios = (await _usuarioRepository.GetAllUsuarios()).Select(s => new UsuarioDto
            {
                Id = s.Id, Nome = s.Nome, Email = s.Email.Endereco, GrupoUsuarioId = s.GrupoUsuarioId, Grupo = s.GrupoUsuario.Nome
            });

            if (!usuarios.Any())
            {
                throw new ArgumentException("Nenhum registro encontrado.");
            }

            return usuarios.ToList();
        }
    }
}
