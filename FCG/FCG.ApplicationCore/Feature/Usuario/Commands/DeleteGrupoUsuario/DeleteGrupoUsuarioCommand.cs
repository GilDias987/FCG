using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Commands.DeleteGrupoUsuario
{
    public class DeleteGrupoUsuarioCommand : IRequest<int>
    {
        public required int Id { get; set; } 
    }
}
