using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.AddGrupoUsuario
{
    public class AddGrupoUsuarioCommand : IRequest<int>
    {
        public required string Nome { get; set; } 
    }
}
