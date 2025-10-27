using FCG.ApplicationCore.Dto.Usuario;
using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.EditGrupoUsuario
{
    public class EditGrupoUsuarioCommand : IRequest<GrupoUsuarioDto>
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
    }
}
