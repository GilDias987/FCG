using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.GetUsuario
{
    public class GetUsuarioQuery : IRequest<GetUsuarioResponse>
    {
        public int Id { get; set; }
    }
}
