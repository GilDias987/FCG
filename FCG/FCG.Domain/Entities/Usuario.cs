using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        #region Propriedades Base
        public  string Nome { get; private set; }
        public  Email Email { get; private set; }
        public  Senha Senha { get; private set; }
        #endregion

        #region Propriedades de Navegação
        public int GrupoUsuarioId { get; set; }
        public GrupoUsuario GrupoUsuario { get; set; }
        public ICollection<UsuarioJogo> UsuarioJogos { get; set; }
        #endregion

        public Usuario()
        {

        }

        public Usuario(string nome, Email email, Senha senha, int grupoUsuarioId)
        {
            Inicializar(nome, email, senha, grupoUsuarioId);
        }

        public void Inicializar(string nome, Email email, Senha senha, int grupoUsuarioId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                {
                    throw new ArgumentException("O nome do usuário não pode ser vazio.");
                }

                Nome  = nome.Trim();
                GrupoUsuarioId = grupoUsuarioId;
            }
            catch(ArgumentException ex)
            {
                throw ex;
            }
        }

        public bool ValidarSenha(string senha)
        {
            try
            {
                return Senha.Verificar(senha);
                
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }
    }
}
