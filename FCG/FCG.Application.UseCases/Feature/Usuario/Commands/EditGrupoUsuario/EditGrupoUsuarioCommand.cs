using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.EditGrupoUsuario
{
    public class EditGrupoUsuarioCommand : IRequest<int>
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
    }
}
