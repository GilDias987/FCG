namespace FCG.Domain.Entities
{
    public class Jogo : BaseEntity
    {
        #region Propriedades Base
        public string Titulo { get; private set; }
        public string? Descricao { get; private set; }
        public decimal? Preco { get; private set; }
        public decimal? Desconto { get; private set; }
        #endregion

        #region Propriedades de Navegação
        public int GeneroId { get; set; }
        public Genero Genero { get; set; }
        public int PlataformaId { get; set; }
        public Plataforma Plataforma { get; set; }
        public ICollection<UsuarioJogo> UsuarioJogos { get; set; }
        #endregion

        /// <summary>
        /// Jogo
        /// </summary>
        public Jogo()
        { 
        }

        /// <summary>
        /// Jogo
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="descricao"></param>
        /// <param name="preco"></param>
        /// <param name="desconto"></param>
        /// <param name="generoId"></param>
        /// <param name="plataformaId"></param>
        public Jogo(string titulo, string descricao, decimal preco, decimal desconto, int generoId, int plataformaId)
        {
            Titulo = titulo;
            Descricao = descricao;
            Preco = preco;
            Desconto = desconto;
            GeneroId = generoId;
            PlataformaId = plataformaId;
        }
    }
}
