using FCG.Domain.Common.Exceptions;
using FCG.Domain.Entities;
using FluentAssertions;

namespace FCG.Domain.Tests
{
    public class JogoTest
    {
        [Theory(DisplayName = "Validar se o jogo não é válido")]
        [InlineData("","Jogo de corrida mais famoso da antiguidade", 5,0,1,2)]
        [InlineData("Top Gear", "Jo", 5, 0, 1, 2)]
        [InlineData("Top Gear", "Jogo de corrida mais famoso da antiguidade", -1, 0, 1, 2)]
        [InlineData("Top Gear", "Jogo de corrida mais famoso da antiguidade", 5, -1, 1, 2)]
        [InlineData("Top Gear", "Jogo de corrida mais famoso da antiguidade", 5, 101, 1, 2)]
        [InlineData("Top Gear", "Jogo de corrida mais famoso da antiguidade", 5, 0, -1, 2)]
        [InlineData("Top Gear", "Jogo de corrida mais famoso da antiguidade", 5, 0, 2, -1)]
        public void Jogo_Invalido_Throws_ArgumentException(string titulo, string descricao, decimal preco, decimal desconto, int generoId, int plataformaId)
        {
            Assert.Throws<DomainException>(() => new Jogo(titulo,descricao,preco,desconto,generoId, plataformaId));
        }

        [Fact(DisplayName = "Validar se a jogo é válido")]
        public void Jogo_Valido()
        {
            var titulo = "Top Gear";
            var descricao = "Jogo de corrida mais famoso da antiguidade";
            decimal preco = 5;
            decimal desconto = 50;
            int generoId = 1;
            int plataformaId = 2;
            decimal precoComDesconto = 2.5M;

            var objJogo = new Jogo(titulo, descricao, preco, desconto, generoId, plataformaId);
            objJogo.Titulo.Should().Be(titulo);
            objJogo.Descricao.Should().Be(descricao);
            objJogo.Preco.Should().Be(preco);
            objJogo.Desconto.Should().Be(desconto);
            objJogo.GeneroId.Should().Be(generoId);
            objJogo.PlataformaId.Should().Be(plataformaId);
            objJogo.CalcularPrecoComDesconto().Should().Be(precoComDesconto);
        }

        [Fact(DisplayName = "Validar se o desconto do jogo não é válido")]
        public void Jogo_Aplicar_Desconto_Nao_Valido()
        {
            var titulo = "Top Gear";
            var descricao = "Jogo de corrida mais famoso da antiguidade";
            decimal preco = 5;
            decimal desconto = 0;
            int generoId = 1;
            int plataformaId = 2;
            decimal precoComDesconto = 1.5M;

            var objJogo = new Jogo(titulo, descricao, preco, desconto, generoId, plataformaId);
            objJogo.AplicarDesconto(50);

            Assert.NotEqual(precoComDesconto, objJogo.CalcularPrecoComDesconto());
        } 

        [Fact(DisplayName = "Validar se o desconto do jogo é válido")]
        public void Jogo_Aplicar_Desconto_Valido()
        {
            var titulo = "Top Gear";
            var descricao = "Jogo de corrida mais famoso da antiguidade";
            decimal preco = 5;
            decimal desconto = 0;
            int generoId = 1;
            int plataformaId = 2;
            decimal precoComDesconto = 2.5M;

            var objJogo = new Jogo(titulo, descricao, preco, desconto, generoId, plataformaId);
            objJogo.AplicarDesconto(50);

            objJogo.Titulo.Should().Be(titulo);
            objJogo.Descricao.Should().Be(descricao);
            objJogo.Preco.Should().Be(preco);
            objJogo.Desconto.Should().Be(50);
            objJogo.GeneroId.Should().Be(generoId);
            objJogo.PlataformaId.Should().Be(plataformaId);
            objJogo.CalcularPrecoComDesconto().Should().Be(precoComDesconto);
        }
    }
}
