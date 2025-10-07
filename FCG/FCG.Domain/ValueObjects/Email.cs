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
        public string Endereco { get; }

        public Email(string endereco)
        {
            ValidarEmail(endereco);
            ValidarEmailDominio(endereco);
            Endereco = endereco;
        }

        private static void ValidarEmailDominio(string value)
        {
            if (!Regex.IsMatch(value, @"@(fiap\.com\.br|alura\.com\.br|pm3\.com\.br)$"))
                throw new ArgumentException("E-mail deve pertencer aos domínios @fiap.com.br, @alura.com.br ou @pm3.com.br.", nameof(value));
        }

        private static void ValidarEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("E-mail não pode ser vazio.", nameof(value));
        }

        public string Validar(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return "E-mail não pode ser vazio.";

            if (!Regex.IsMatch(value, @"@(fiap\.com\.br|alura\.com\.br|pm3\.com\.br)$"))
                return "E-mail deve pertencer aos domínios @fiap.com.br, @alura.com.br ou @pm3.com.br.";

            return string.Empty;
        }
    }
}
