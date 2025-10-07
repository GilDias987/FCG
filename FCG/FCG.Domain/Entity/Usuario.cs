using FCG.Domain.Entity.Abstract;
using FCG.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FCG.Domain.Entity
{
    public class Usuario : EntityBase
    {
        #region Propriedades Base
        public  string Nome { get; private set; }
        public  string Email { get; private set; }
        public  string Senha { get; private set; }
        #endregion


        #region Propriedades de Navegação
        public int GrupoUsuarioId { get; set; }
        public GrupoUsuario GrupoUsuario { get; set; }
        public ICollection<UsuarioJogo> UsuarioJogos { get; set; }
        #endregion

        public Usuario(string nome, string email, string senha, int grupoUsuarioId)
        {
            Salvar(nome, email, senha, grupoUsuarioId);
        }

        public void Salvar(string nome, string email, string senha, int grupoUsuarioId)
        {
            ValidaUsuario(nome, email, senha);

            Email objEmail = new Email(email);
            Senha objSenha = new Senha(senha);
            Nome = nome;
            Email = objEmail.Endereco;
            Senha = objSenha.TextHash;
            GrupoUsuarioId = grupoUsuarioId;
        }

        private void ValidaUsuario(string nome, string email, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("O nome não pode ser vazio.");
            }

            Email objEmail = new Email(email);

            if (!string.IsNullOrWhiteSpace(objEmail.Validar(email)))
            {
                throw new ArgumentException(objEmail.Validar(email));
            }

            Senha objSenha = new Senha(senha);

            if (!string.IsNullOrWhiteSpace(objSenha.Validar(senha)))
            {
                throw new ArgumentException(objSenha.Validar(senha));
            }
        }
    }
}
