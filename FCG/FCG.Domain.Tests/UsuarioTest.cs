using FCG.Domain.Common.Exceptions;
using FCG.Domain.Entities;
using FCG.Domain.ValueObjects;
using FluentAssertions;

namespace FCG.Domain.Tests
{
    public class UsuarioTest
    {
        [Theory(DisplayName = "Validar se o email não é válido")]
        [InlineData("")]
        [InlineData("email")]
        [InlineData("email@gmail.com")]
        public void Email_Invalido_Throws_ArgumentException(string email)
        {
            Assert.Throws<DomainException>(() => new Email(email));
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
            Assert.Throws<DomainException>(() => new Senha(senha));
        }


        [Fact(DisplayName = "Validar se a senha é válida")]
        public void Senha_Valida()
        {
            var senha = "Carlos@987";
            var objsenha = new Senha(senha);

            Assert.True(objsenha.Valor.Length > 0);
        }

        [Theory(DisplayName = "Validar se o usuário não é válido")]
        [InlineData("Alexandre","alexandre@fiap.com.br", "Alexandre@123",-1)]
        [InlineData("", "alexandre@fiap.com.br", "Alexandre@123", 1)]
        public void Usuario_Invalido_Throws_ArgumentException(string nome, string email,string senha, int grupoUsuarioId)
        {
            var objEmail = new Email(email);
            var objSenha = new Senha(senha);

            Assert.Throws<DomainException>(() => new Usuario(nome, objEmail, objSenha, grupoUsuarioId));
        }

        [Theory(DisplayName = "Validar se o usuário é válido")]
        [InlineData("Alexandre", "alexandre@fiap.com.br", "Alexandre@123", 1)]
        [InlineData("Gil Dias", "gil@fiap.com.br", "Carlos@987", 2)]
        public void Usuario_Valido(string nome, string email, string senha, int grupoUsuarioId)
        {
            var objEmail = new Email(email);
            var objSenha = new Senha(senha);
            var usuario = new Usuario(nome, objEmail, objSenha, grupoUsuarioId);
            usuario.Nome.Should().Be(nome);
        }
    }
}
