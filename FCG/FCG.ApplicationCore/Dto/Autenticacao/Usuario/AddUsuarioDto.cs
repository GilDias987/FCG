using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Dto.Autenticacao.Usuario
{
    public class AddUsuarioDto
    {
        public required string Nome { get; set; }

        public required string Email { get; set; }

        public required string Senha { get; set; }

        public int GrupoUsuarioId { get; set; }
    }
}
