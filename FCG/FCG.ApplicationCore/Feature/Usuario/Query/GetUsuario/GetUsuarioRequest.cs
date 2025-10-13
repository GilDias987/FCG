using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario
{
    public class GetUsuarioRequest : IRequest<GetUsuarioResponse>
    {
        public int Id { get; set; }
    }
}
