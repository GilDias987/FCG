using FCG.Domain.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Entity
{
    public class Plataforma : EntityBase
    {
        #region Propriedades Base
        public required string Titulo { get; set; }
        #endregion

        #region Propriedades Navegacao
        public ICollection<Jogo> Jogos { get; set; }
        #endregion
    }
}
