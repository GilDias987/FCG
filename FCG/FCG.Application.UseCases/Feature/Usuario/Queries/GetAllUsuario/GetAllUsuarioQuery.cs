using MediatR;

// Dependências
using FCG.ApplicationCore.Dto.Usuario;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetAllUsuario
{
    public class GetAllUsuarioQuery : IRequest<List<UsuarioDto>>
    {
    }
}
