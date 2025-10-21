using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Commands.EditGrupoUsuario
{
    public class EditGrupoUsuarioCommand : IRequest<int>
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
    }
}
