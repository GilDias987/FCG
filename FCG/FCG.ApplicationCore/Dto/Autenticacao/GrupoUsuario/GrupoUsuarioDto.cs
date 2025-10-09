using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Dto.Autenticacao.GrupoUsuario
{
    public class GrupoUsuarioDto
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
    }
}
