using FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Query.GetGrupoUsuario
{
    public class GetGrupoUsuarioRequest : IRequest<GrupoUsuarioResponse>
    {
        public int Id { get; set; }
    }
}
