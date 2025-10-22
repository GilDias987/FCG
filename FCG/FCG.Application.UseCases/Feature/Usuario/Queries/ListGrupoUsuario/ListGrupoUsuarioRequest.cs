using FCG.Application.UseCases.Feature.Usuario.Queries;
using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.ListGrupoUsuario
{
    public class ListGrupoUsuarioRequest : IRequest<List<GrupoUsuarioResponse>>
    {
    }
}
