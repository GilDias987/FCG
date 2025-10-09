using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Command.DeleteGrupoUsuario
{
    public class DeleteGrupoUsuarioCommand : IRequest<int>
    {
        public required int Id { get; set; } 
    }
}
