using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetUsuario
{
    public class GetUsuarioQuery : IRequest<UsuarioDto>
    {
        public int Id { get; set; }
    }
}
