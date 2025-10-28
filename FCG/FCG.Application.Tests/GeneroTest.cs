// Dependências
using FCG.Application.UseCases.Feature.Jogo.Commands.AddGenero;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using MediatR;
using Moq;

namespace FCG.Application.Tests
{
    public class GeneroTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IGeneroRepository> _generoRepositoryMock;

        public GeneroTest()
        {
            _mediatorMock = new();
            _generoRepositoryMock = new();
        }

        [Fact(DisplayName = "Verificar se o gênero de jogo não existe")]
        public async Task GetGeneroNaoExiste()
        {
            // Arrange
            var query   = new GetGeneroQuery { Id = 100 };
            var handler = new GetGeneroQueryHandler(_generoRepositoryMock.Object);

            // Act
            _generoRepositoryMock.Setup(rep => rep.GetGeneroIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Genero?)null);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(query, default));
        }

        [Fact(DisplayName = "Verificar se o gênero de jogo existe")]
        public async Task GetGeneroExiste()
        {
            // Arrange
            var result  = true;
            var query   = new GetGeneroQuery { Id = 1 };
            var handler = new GetGeneroQueryHandler(_generoRepositoryMock.Object);
            var oGenero = new Genero("Ação");

            _generoRepositoryMock
                .Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(oGenero);

            // Act
            var hGenero = await handler.Handle(query, default);

            // Assert
            Assert.Equal(result, hGenero != null);
        }

        [Fact(DisplayName = "Adicionar gênero de jogo não válido com título existente")]
        public async Task AddGeneroComTituloExistente()
        {
            // Arrange
            var oGenero   = new AddGeneroCommand { Titulo = "Ação" };
            var validator = new AddGeneroCommandValidator();

            // Act
            _generoRepositoryMock
                .Setup(rep => rep.ExistsByAsync(e => e.Titulo.ToLower() == It.IsAny<string>().ToLower()))
                .ReturnsAsync(true);

            // Assert
            var result = await validator.ValidateAsync(oGenero);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Já existe um gênero de jogo com esse título.", erros);
        }
    }
}
