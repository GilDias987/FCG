using FCG.Domain.Common.Exceptions;
using FCG.Domain.Entities;
using FluentAssertions;

namespace FCG.Domain.Tests
{
    public class GeneroTest
    {
        [Fact(DisplayName = "Validar se o genero não é válido")]
        public void Genero_Usuario_Invalido_Throws_ArgumentException()
        {
            var genero = "";
            Assert.Throws<DomainException>(() => new Genero(genero));
        }

        [Fact(DisplayName = "Validar se o genro é válido")]
        public void Genero_Usuario_Valido()
        {
            var nomeGenero = "Aventura";
            var genero = new Genero(nomeGenero);
            genero.Titulo.Should().Be(nomeGenero);
        }
    }
}
