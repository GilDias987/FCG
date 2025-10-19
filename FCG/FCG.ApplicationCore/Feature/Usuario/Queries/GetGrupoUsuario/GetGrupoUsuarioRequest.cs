using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.GetGrupoUsuario
{
    public class GetGrupoUsuarioRequest : IRequest<GrupoUsuarioResponse>
    {
        public int Id { get; set; }
    }
}
