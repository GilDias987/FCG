using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects
{
    public record class Email
    {
        /// <summary>
        /// Endereço
        /// </summary>
        public string Endereco { get; }

        /// <summary>
        /// E-mail
        /// </summary>
        /// <param name="endereco"></param>
        public Email(string endereco)
        {
            ValidarEmail(endereco);
            ValidarEmailDominio(endereco);
            Endereco = endereco.Trim();
        }

        /// <summary>
        /// Validar E-mail e Domínio
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidarEmailDominio(string value)
        {
            if (!Regex.IsMatch(value, @"@(fiap\.com\.br|alura\.com\.br|pm3\.com\.br)$"))
                throw new ArgumentException("E-mail deve pertencer aos domínios @fiap.com.br, @alura.com.br ou @pm3.com.br.");
        }

        /// <summary>
        /// Validar E-mail
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidarEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("E-mail não pode ser vazio.");
        }
    }
}
