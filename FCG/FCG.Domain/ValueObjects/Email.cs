using FCG.Domain.Common.Exceptions;
using FCG.Domain.Common.Validations;
using System.Text.RegularExpressions;

namespace FCG.Domain.ValueObjects
{
    public record class Email
    {
        public string Endereco { get; }

        public Email()
        {

        }

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
            Guard.Against<DomainException>(!Regex.IsMatch(value, @"@(fiap\.com\.br|alura\.com\.br|pm3\.com\.br)$"),
                                                                    "E-mail deve pertencer aos domínios @fiap.com.br, @alura.com.br ou @pm3.com.br.");
        }

        /// <summary>
        /// Validar E-mail
        /// </summary>
        /// <param name="value"></param>
        /// <exception cref="ArgumentException"></exception>
        private static void ValidarEmail(string value)
        {
            Guard.Against<DomainException>(string.IsNullOrWhiteSpace(value), "E-mail não pode ser vazio.");
        }

        public string ObterDominio()
        {
            // Expressão regular para extrair o domínio
            string pattern = @"@([a-zA-Z0-9.-]+)";

            // Tenta encontrar o padrão no e-mail
            Match match = Regex.Match(Endereco, pattern);

            // Se o padrão for encontrado, retorna o domínio
            if (match.Success && match.Groups.Count > 1)
            {
                return match.Groups[1].Value;
            }

            // Retorna nulo ou uma string vazia se o domínio não for encontrado
            return "";
        }

    }
}
