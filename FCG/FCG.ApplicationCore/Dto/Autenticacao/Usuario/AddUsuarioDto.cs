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
        [Required(ErrorMessage = "O nome do usuário é obrigatório", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Usuário")]
        public required string Nome { get; set; }
        [Required(ErrorMessage = "O email do usuário é obrigatório", AllowEmptyStrings = false)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "O senha do usuário é obrigatório", AllowEmptyStrings = false)]
        public required string Senha { get; set; }
    }
}
