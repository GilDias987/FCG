using FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario;
using FCG.ApplicationCore.Feature.Usuario.Query.ListGrupoUsuario;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Query.LIstGrupoUsuario
{
    public class ListGrupoUsuarioRequest : IRequest<List<GrupoUsuarioResponse>>
    {
    }
}
