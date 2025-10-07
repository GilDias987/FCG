using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FCG.Domain.ValueObjects
{
    public class Senha
    {
        /// <summary>
        /// Texto Hash
        /// </summary>
        public string TextHash
        { get; }

        /// <summary>
        /// Texto Senha
        /// </summary>
        public string TextSenha
        { get; private set; }

        /// <summary>
        /// Senha
        /// </summary>
        /// <param name="textSenha"></param>
        public Senha(string textSenha)
        {
            Validar(textSenha);

            TextSenha = textSenha;
            TextHash = BCrypt.Net.BCrypt.HashPassword(textSenha);
        }

        /// <summary>
        /// Verificar senha corresponde ao hash armazenado.
        /// </summary>
        public bool Verificar(string textSenha)
        {
            return BCrypt.Net.BCrypt.Verify(textSenha, TextHash);
        }

        /// <summary>
        /// Validar senha no momento da criação.
        /// </summary>
        private static void ValidarSenha(string senha)
        {
            if (senha.Length < 8)
                throw new ArgumentException("Senha deve ter pelo menos 8 caracteres.");

            if (!Regex.IsMatch(senha, "[a-zA-Z]"))
                throw new ArgumentException("Senha deve conter pelo menos uma letra.");

            if (!Regex.IsMatch(senha, "[0-9]"))
                throw new ArgumentException("Senha deve conter pelo menos um número.");

            if (!Regex.IsMatch(senha, "[^a-zA-Z0-9]"))
                throw new ArgumentException("Senha deve conter pelo menos um caractere especial.");
        }

        public string Validar(string senha)
        {
            if (senha.Length < 8)
                return "Senha deve ter pelo menos 8 caracteres.";

            if (!Regex.IsMatch(senha, "[a-zA-Z]"))
                return "Senha deve conter pelo menos uma letra.";

            if (!Regex.IsMatch(senha, "[0-9]"))
                return "Senha deve conter pelo menos um número.";

            if (!Regex.IsMatch(senha, "[^a-zA-Z0-9]"))
                return "Senha deve conter pelo menos um caractere especial.";

            return string.Empty;
        }
    }
}
