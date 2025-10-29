using FCG.Domain.Common.Exceptions;
using FCG.Domain.Common.Validations;
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
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(nome), "O nome do usuário não pode ser vazio.");
            Guard.AgainstEmptyId(grupoUsuarioId, "Grupo Usuario Id");

            Nome = nome.Trim();
            GrupoUsuarioId = grupoUsuarioId;
            Email = email;
            Senha = senha;

        }

        public bool ValidarSenha(string senha)
        {
            return Senha.Verificar(senha);
        }
    }
}
