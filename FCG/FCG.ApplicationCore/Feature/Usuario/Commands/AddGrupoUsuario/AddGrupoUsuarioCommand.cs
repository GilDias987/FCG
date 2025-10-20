using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Commands.AddGrupoUsuario
{
    public class AddGrupoUsuarioCommand : IRequest<int>
    {
        public required string Nome { get; set; } 
    }
}
