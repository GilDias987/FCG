// Dependências
using FCG.Application.UseCases.Feature.Jogo.Commands.AddGenero;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeleteGenero;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditGenero;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetGenero;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FluentAssertions;
using MediatR;
using Moq;
using System.Linq.Expressions;

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

        [Fact(DisplayName = "Adicionar gênero de jogo não válido com título existente.")]
        public async Task AddGeneroComTituloExistente()
        {
            // Arrange
            var oGenero   = new AddGeneroCommand { Titulo = "Ação" };
            var validator = new AddGeneroValidator(_generoRepositoryMock.Object);

            // Act

            _generoRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Genero, bool>>>()))
              .ReturnsAsync(true);

            // Assert
            var result = await validator.ValidateAsync(oGenero);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Já existe um gênero de jogo com esse título.", erros);

        }

        [Fact(DisplayName = "Adicionar gênero de jogo não válido sem título.")]
        public async Task AddGeneroSemTitulo()
        {
            // Arrange
            var grupoUsuaro = new AddGeneroCommand { Titulo = "" };
            var validator = new AddGeneroValidator(_generoRepositoryMock.Object);

            // Act
            _generoRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Genero, bool>>>()))
            .ReturnsAsync(false);

            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o titulo do genero.", erros);
        }

        [Fact(DisplayName = "Adicionar gênero de jogo válido.")]
        public async Task AddGeneroValido()
        {
            // Arrange
            var command = new AddGeneroCommand { Titulo = "Ação" };
            var validator = new AddGeneroValidator(_generoRepositoryMock.Object);

            // Act
            _generoRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Genero, bool>>>()))
             .ReturnsAsync(false);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Adicionar gênero de jogo.")]
        public async Task AddGenero()
        {
            // Arrange
            var command = new AddGeneroCommand { Titulo = "Ação" };
            var handler = new AddGeneroCommandHandler(_generoRepositoryMock.Object);
            var genero = new Genero("Ação");

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Genero>()))
                .ReturnsAsync(genero);

            var generoDto = await handler.Handle(command, default);

            // Assert
            generoDto.Titulo.Should().Be(genero.Titulo);
        }

        [Fact(DisplayName = "Editar gênero de jogo não válido sem titulo.")]
        public async Task EditGeneroSemTitulo()
        {
            // Arrange
            var command   = new EditGeneroCommand { Titulo = "" };
            var validator = new EditGeneroValidator(_generoRepositoryMock.Object);

            // Act
            _generoRepositoryMock
              .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(new Genero("Ação"));

            _generoRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Genero, bool>>>()))
                .ReturnsAsync(false);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o titulo do gênero.", erros);
        }

        [Fact(DisplayName = "Editar gênero de jogo não válido com título existente.")]
        public async Task EditGeneroComNomeExistente()
        {
            // Arrange
            var command   = new EditGeneroCommand { Titulo = "" };
            var validator = new EditGeneroValidator(_generoRepositoryMock.Object);

            // Act 
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Ação"));

            _generoRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Genero, bool>>>()))
            .ReturnsAsync(true);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Já existe um gênero com esse título.", erros);
        }

        [Fact(DisplayName = "Editar gênero de jogo válido.")]
        public async Task EditGeneroValido()
        {
            // Arrange
            var command = new EditGeneroCommand { Titulo = "Ação" };
            var validator = new EditGeneroValidator(_generoRepositoryMock.Object);

            // Act

            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Genero("Ação"));

            _generoRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Genero, bool>>>()))
            .ReturnsAsync(false);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Editar gênero de jogo.")]
        public async Task EditGenero()
        {
            // Arrange
            var command = new EditGeneroCommand { Titulo = "Ação" };
            var handler = new EditGeneroCommandHandler(_generoRepositoryMock.Object);
            var genero = new Genero("Ação");

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(genero);

            _generoRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Genero>()));

            var generoDto = await handler.Handle(command, default);

            // Assert
            generoDto.Titulo.Should().Be(genero.Titulo);
        }

        [Fact(DisplayName = "Deletar gênero de jogo inexistente.")]
        public async Task DeleteGeneroInexistente()
        {
            // Arrange
            var grupoUsuaro = new DeleteGeneroCommand { Id = 1 };
            var validator = new DeleteGeneroValidator(_generoRepositoryMock.Object);

            // Act

            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Genero)null);


            var result = await validator.ValidateAsync(grupoUsuaro);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id do genero informado não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Deletar gênero de jogo existente.")]
        public async Task DeleteGeneroExistente()
        {
            // Arrange
            var command = new DeleteGeneroCommand { Id = 1 };
            var validator = new DeleteGeneroValidator(_generoRepositoryMock.Object);
            var genero = new Genero("Ação");

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(genero);


            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Deletar gênero de jogo.")]
        public async Task DeleteGenero()
        {
            // Arrange
            var command = new DeleteGeneroCommand { Id = 1 };
            var handler = new DeleteGeneroCommandHandler(_generoRepositoryMock.Object);
            var genero = new Genero("Ação");

            // Act
            _generoRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(genero);

            _generoRepositoryMock
                .Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            var deletado = await handler.Handle(command, default);

            // Assert
            Assert.True(deletado);
        }
    }
}
