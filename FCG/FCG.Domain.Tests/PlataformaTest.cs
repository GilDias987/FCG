using FCG.Domain.Common.Exceptions;
using FCG.Domain.Entities;
using FluentAssertions;

namespace FCG.Domain.Tests
{
    public class PlataformaTest
    {
        [Fact(DisplayName = "Validar se o plataforma não é válida")]
        public void Plataforma_Usuario_Invalido_Throws_ArgumentException()
        {
            var plataforma = "";
            Assert.Throws<DomainException>(() => new Plataforma(plataforma));
        }

        [Fact(DisplayName = "Validar se a plataforma é válida")]
        public void Plataforma_Usuario_Valido()
        {
            var nomePlataforma = "PS5";
            var plataforma = new Plataforma(nomePlataforma);
            plataforma.Titulo.Should().Be(nomePlataforma);
        }
    }
}
