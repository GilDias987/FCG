using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Dto.Autenticacao.Usuario
{
    
    public class UsuarioDto
    {
        public required string Email { get; set; }
        public required string Grupo { get; set; }
    }
}
