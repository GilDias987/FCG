using FCG.Domain.Entity.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Domain.Entity
{
    public class Jogo : EntityBase
    {
        #region Propriedades Base
        public required string Titulo { get; set; }
        public string? Descricao { get; set; }
        public decimal? Preco { get; set; }
        public decimal? Desconto { get; set; }
        #endregion

        #region Propriedades de Navegação
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }

        public int PlataformaId { get; set; }
        public Plataforma Plataforma { get; set; }

        public ICollection<UsuarioJogo> UsuarioJogos { get; set; }
        #endregion
    }
}
