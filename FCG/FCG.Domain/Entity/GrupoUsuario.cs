using FCG.Domain.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Entity
{
    public class GrupoUsuario : EntityBase
    {   
        #region Propriedades Base
        public required string Nome { get; set; }
        #endregion

        #region Propriedades Navegacao
        public ICollection<Usuario> Usuarios { get; set; }
        #endregion
    }
}
