using FCG.Domain.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Entity
{
    public class UsuarioJogo : EntityBase
    {
        #region Propriedades de Navegação
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int JogoId { get; set; }
        public Jogo Jogo { get; set; }
        #endregion
    }
}
