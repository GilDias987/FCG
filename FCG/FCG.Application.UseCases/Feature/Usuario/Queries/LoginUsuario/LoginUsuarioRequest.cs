using FCG.Application.UseCases.Feature.Usuario.Queries;
using FCG.ApplicationCore.Dto.Usuario;
using MediatR;

namespace FCG.Application.UseCases.Feature.Usuario.Queries.LoginUsuario
{
    public class LoginUsuarioRequest : IRequest<UsuarioDto>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
