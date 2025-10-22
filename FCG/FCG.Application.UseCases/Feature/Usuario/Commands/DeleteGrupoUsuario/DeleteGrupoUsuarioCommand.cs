using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.DeleteGrupoUsuario
{
    public class DeleteGrupoUsuarioCommand : IRequest<int>
    {
        public required int Id { get; set; } 
    }
}
