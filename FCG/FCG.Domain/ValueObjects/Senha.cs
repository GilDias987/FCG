using FCG.Domain.Common.Exceptions;
using FCG.Domain.Common.Validations;
using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects
{
    public record class Senha
    {
        /// <summary>
        /// Texto Senha
        /// </summary>
        public string Valor { get; private set; }


        /// <summary>
        /// Senha
        /// </summary>
        /// <param name="textSenha"></param>
        public Senha()
        {
     
        }

        /// <summary>
        /// Senha
        /// </summary>
        /// <param name="textSenha"></param>
        public Senha(string valor)
        {
            string objSenha = valor.Trim();
            ValidarSenha(objSenha);
            Valor = BCrypt.Net.BCrypt.HashPassword(objSenha);
        }

        /// <summary>
        /// Verificar senha corresponde ao hash armazenado.
        /// </summary>
        public bool Verificar(string textSenha)
        {
            return BCrypt.Net.BCrypt.Verify(textSenha, Valor);
        }

        /// <summary>
        /// Validar senha no momento da criação.
        /// </summary>
        private static void ValidarSenha(string senha)
        {
            Guard.Against<DomainException>(senha.Length < 8, "Senha deve ter pelo menos 8 caracteres.");
            Guard.Against<DomainException>(!Regex.IsMatch(senha, "[a-zA-Z]"), "Senha deve conter pelo menos uma letra.");
            Guard.Against<DomainException>(!Regex.IsMatch(senha, "[0-9]"), "Senha deve conter pelo menos um número.");
            Guard.Against<DomainException>(!Regex.IsMatch(senha, "[0-9]"), "Senha deve conter pelo menos um caractere especial.");
        }

    }
}
