namespace FCG.Domain.Entities
{
    public class GrupoUsuario : BaseEntity
    {   
        #region Propriedades Base
        public string Nome { get; private set; }
        #endregion

        #region Propriedades Navegacao
        public ICollection<Usuario>? Usuarios { get; set; }
        #endregion

        public GrupoUsuario(string nome)
        {
            Inicializar(nome);
        }

        public void Inicializar(string nome)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                {
                    throw new ArgumentException("O nome do grupo não pode ser vazio.");
                }

                Nome = nome.Trim();
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }
    }
}
