using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;
using FCG.ApplicationCore.Dto.Jogo;

namespace FCG.ApplicationCore.Feature.Usuario.Commands.AddUsuario
{
    public class AddUsuarioCommand : IRequest<UsuarioDto>
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public int UsuarioGrupoId { get; set; }
    }
}
