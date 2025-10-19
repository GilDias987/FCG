using FCG.Domain.ValueObjects;

namespace FCG.Domain.Entities
{
    public class Usuario : BaseEntity
    {
        #region Propriedades Base
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string Senha { get; private set; }
        #endregion

        #region Propriedades de Navegação
        public int GrupoUsuarioId { get; set; }
        public GrupoUsuario GrupoUsuario { get; set; }
        public ICollection<UsuarioJogo> UsuarioJogos { get; set; }
        #endregion

        public Usuario()
        {

        }

        public Usuario(string nome, string email, string senha, int grupoUsuarioId)
        {
            Inicializar(nome, email, senha, grupoUsuarioId);
        }

        public void Inicializar(string nome, string email, string senha, int grupoUsuarioId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nome))
                {
                    throw new ArgumentException("O nome do usuário não pode ser vazio.");
                }

                Email objEmail = new Email(email);
                Senha objSenha = new Senha(senha);

                Nome  = nome.Trim();
                Email = objEmail.Endereco;
                Senha = objSenha.TextHash;
                GrupoUsuarioId = grupoUsuarioId;
            }
            catch(ArgumentException ex)
            {
                throw ex;
            }
        }

        public bool ValidarSenha(string senha, string senhaHash)
        {
            try
            {
                Senha objSenha = new Senha(senha, senhaHash);
                return objSenha.Verificar(senha);
                
            }
            catch (ArgumentException ex)
            {
                throw ex;
            }
        }
    }
}
