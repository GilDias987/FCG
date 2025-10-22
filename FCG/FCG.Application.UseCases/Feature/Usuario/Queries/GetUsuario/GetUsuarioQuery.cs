using FCG.Application.UseCases.Feature.Usuario.Queries;
using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.GetUsuario
{
    public class GetUsuarioQuery : IRequest<GetUsuarioResponse>
    {
        public int Id { get; set; }
    }
}
