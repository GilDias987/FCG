using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.DeleteGrupoUsuario
{
    public class DeleteGrupoUsuarioCommand : IRequest<bool>
    {
        public required int Id { get; set; } 
    }
}
