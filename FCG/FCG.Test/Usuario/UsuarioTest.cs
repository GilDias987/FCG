using FCG.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCG.Test.Usuario
{
    public class UsuarioTest
    {
        [Theory(DisplayName = "Validar se o email não é válido")]
        [InlineData("")]
        [InlineData("email")]
        [InlineData("email@gmail.com")]
        public void Email_Invalido_Throws_ArgumentException(string email)
        {
            Assert.Throws<ArgumentException>(() => new Email(email));
        }

        [Fact(DisplayName = "Validar se o email é válido")]
        public void Email_Valido()
        {
            var email = "gil@fiap.com.br";
            var objEmail = new Email(email);
            Assert.Equal(email, objEmail.Endereco);
        }

        [Theory(DisplayName = "Validar se a senha é válida")]
        [InlineData("")]
        [InlineData("emee")]
        [InlineData("senhaa")]
        [InlineData("Senhaaa")]
        [InlineData("@Senhaaa")]
        public void Senha_Invalida_Throws_ArgumentException(string senha)
        {
            Assert.Throws<ArgumentException>(() => new Senha(senha));
        }


    }
}
