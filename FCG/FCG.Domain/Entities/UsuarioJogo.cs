namespace FCG.Domain.Entities
{
    public class UsuarioJogo : BaseEntity
    {
        #region Propriedades de Navegação
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int JogoId { get; set; }
        public Jogo Jogo { get; set; }
        #endregion
    }
}
