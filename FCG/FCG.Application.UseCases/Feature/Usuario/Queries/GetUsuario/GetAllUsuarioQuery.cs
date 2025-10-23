using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetUsuario
{
    public class GetAllUsuarioQuery : IRequest<List<UsuarioDto>>
    {
    }
}
