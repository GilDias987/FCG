using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Feature.Usuario.Command.EditGrupoUsuario
{
    public class EditGrupoUsuarioCommand : IRequest<int>
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
    }
}
