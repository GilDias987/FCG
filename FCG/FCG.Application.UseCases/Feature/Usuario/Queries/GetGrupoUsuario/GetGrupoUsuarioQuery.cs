using FCG.Application.UseCases.Feature.Usuario.Queries;
using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetGrupoUsuario
{
    public class GetGrupoUsuarioQuery : IRequest<GrupoUsuarioResponse>
    {
        public int Id { get; set; }
    }
}
