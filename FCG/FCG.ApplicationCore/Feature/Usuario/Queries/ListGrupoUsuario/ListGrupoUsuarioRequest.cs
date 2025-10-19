using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.ListGrupoUsuario
{
    public class ListGrupoUsuarioRequest : IRequest<List<GrupoUsuarioResponse>>
    {
    }
}
