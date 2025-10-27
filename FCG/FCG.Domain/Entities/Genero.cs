using FCG.Domain.Common.Exceptions;
using FCG.Domain.Common.Validations;
using System.Runtime.ConstrainedExecution;

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

        public Genero(string titulo)
        {
            Inicializar(titulo);
        }

        public void Inicializar(string titulo)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(titulo), "O titulo do genero não pode ser vazio.");
            Titulo = titulo.Trim();
        }
    }
}
