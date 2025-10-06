using FCG.Domain.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Entity
{
    public class Usuario : EntityBase
    {
        #region Propriedades Base
        public required string Nome { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
        #endregion


        #region Propriedades de Navegação
        public int GrupoUsuarioId { get; set; }
        public GrupoUsuario GrupoUsuario { get; set; }
        public ICollection<UsuarioJogo> UsuarioJogos { get; set; }
        #endregion
    }
}
