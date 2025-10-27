using FCG.Domain.Common.Exceptions;
using FCG.Domain.Common.Validations;

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

        public Plataforma(string titulo) 
        {
            Inicializar(titulo);
        }

        public void Inicializar(string titulo)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(titulo), "O titulo da plataforma não pode ser vazio.");
            Titulo = titulo.Trim();
        }
    }
}
