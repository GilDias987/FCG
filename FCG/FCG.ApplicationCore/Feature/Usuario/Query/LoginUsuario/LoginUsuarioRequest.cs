using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Query.LoginUsuario
{
    public class LoginUsuarioRequest : IRequest<GetUsuarioResponse>
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
