namespace FCG.Domain.Entities
{
    public class Genero : BaseEntity
    {
        #region Propriedades Base
        public string Titulo { get; private set; }
        #endregion

        #region Propriedades Navegacao
        public ICollection<Jogo> Jogos { get; set; }
        #endregion

        /// <summary>
        /// Gênero
        /// </summary>
        /// <param name="titulo"></param>
        public Genero(string titulo)
        {
            Titulo = titulo;
        }
    }
}
