using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Commands.DeleteUsuario
{
    public class DeleteUsuarioCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }
}
