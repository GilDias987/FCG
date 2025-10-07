using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.ApplicationCore.Dto.Autenticacao.GrupoUsuario
{
    public class AddGrupoUsuarioDto
    {

        [Required(ErrorMessage = "O nome do grupo usuário é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Grupo Usuário")]
        public required string Nome { get; set; }
    }
}
