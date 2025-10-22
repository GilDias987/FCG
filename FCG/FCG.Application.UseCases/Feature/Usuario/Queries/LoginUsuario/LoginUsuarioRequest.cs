using FCG.Application.UseCases.Feature.Usuario.Queries;
using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.LoginUsuario
{
    public class LoginUsuarioRequest : IRequest<GetUsuarioResponse>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
