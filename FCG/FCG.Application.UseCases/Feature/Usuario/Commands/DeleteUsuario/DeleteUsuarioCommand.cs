using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.DeleteUsuario
{
    public class DeleteUsuarioCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
