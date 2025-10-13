using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            if (senha.Length < 8)
                throw new ArgumentException("Senha deve ter pelo menos 8 caracteres.");

            if (!Regex.IsMatch(senha, "[a-zA-Z]"))
                throw new ArgumentException("Senha deve conter pelo menos uma letra.");

            if (!Regex.IsMatch(senha, "[0-9]"))
                throw new ArgumentException("Senha deve conter pelo menos um número.");

            if (!Regex.IsMatch(senha, "[^a-zA-Z0-9]"))
                throw new ArgumentException("Senha deve conter pelo menos um caractere especial.");
        }

    }
}
