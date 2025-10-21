using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.GetGrupoUsuario
{
    public class GetGrupoUsuarioQuery : IRequest<GrupoUsuarioResponse>
    {
        public int Id { get; set; }
    }
}
