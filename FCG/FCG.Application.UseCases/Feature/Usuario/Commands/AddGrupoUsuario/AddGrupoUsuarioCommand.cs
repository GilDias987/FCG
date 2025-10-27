using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;

namespace FCG.Application.UseCases.Feature.Usuario.Commands.AddGrupoUsuario
{
    public class AddGrupoUsuarioCommand : IRequest<GrupoUsuarioDto>
    {
        public required string Nome { get; set; } 
    }
}
