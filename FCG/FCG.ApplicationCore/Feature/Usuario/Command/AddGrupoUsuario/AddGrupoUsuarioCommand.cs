using FCG.ApplicationCore.Feature.Usuario.Query.GetUsuario;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Command.AddGrupoUsuario
{
    public class AddGrupoUsuarioCommand : IRequest<int>
    {
        public required string Nome { get; set; }
    }
}
