using MediatR;

namespace FCG.ApplicationCore.Feature.Usuario.Queries.LoginUsuario
{
    public class LoginUsuarioRequest : IRequest<GetUsuarioResponse>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
