namespace FCG.Domain.Entities
{
    public class Plataforma : BaseEntity
    {
        #region Propriedades Base
        public string Titulo { get; private set; }
        #endregion

        #region Propriedades Navegacao
        public ICollection<Jogo> Jogos { get; set; }
        #endregion

        /// <summary>
        /// Plataforma
        /// </summary>
        /// <param name="titulo"></param>
        public Plataforma(string titulo) 
        {
            Titulo = titulo;
        }
    }
}
