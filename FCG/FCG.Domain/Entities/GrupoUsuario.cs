using FCG.Domain.Common.Exceptions;
using FCG.Domain.Common.Validations;

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

        /// <summary>
        /// Grupo do usuário
        /// </summary>
        /// <param name="nome"></param>
        public GrupoUsuario(string nome)
        {
            Inicializar(nome);
        }

        public void Inicializar(string nome)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(nome), "O nome do grupo não pode ser vazio.");
            Nome = nome.Trim();
        }
    }
}
