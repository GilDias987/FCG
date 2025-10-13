using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FCG.Domain.ValueObjects
{
    public record class Email
    {
        public  string Endereco { get; }

        public Email()
        {

        }

        public Email(string endereco)
        {
            ValidarEmail(endereco);
            ValidarEmailDominio(endereco);
            Endereco = endereco.Trim();
        }

        private static void ValidarEmailDominio(string value)
        {
            if (!Regex.IsMatch(value, @"@(fiap\.com\.br|alura\.com\.br|pm3\.com\.br)$"))
                throw new ArgumentException("E-mail deve pertencer aos domínios @fiap.com.br, @alura.com.br ou @pm3.com.br.");
        }

        private static void ValidarEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("E-mail não pode ser vazio.");
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
