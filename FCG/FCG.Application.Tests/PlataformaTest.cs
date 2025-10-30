using FCG.Application.UseCases.Feature.Jogo.Commands.AddPlataforma;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeleteGenero;
using FCG.Application.UseCases.Feature.Jogo.Commands.DeletePlataforma;
using FCG.Application.UseCases.Feature.Jogo.Commands.EditPlataforma;
using FCG.Application.UseCases.Feature.Jogo.Queries.GetPlataforma;
using FCG.ApplicationCore.Interface.Repository;
using FCG.Domain.Entities;
using FluentAssertions;
using MediatR;
using Moq;
using System.Linq.Expressions;

namespace FCG.Application.Tests
{
    public class PlataformaTest
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly Mock<IPlataformaRepository> _plataformaRepositoryMock;

        public PlataformaTest()
        {
            _mediatorMock = new();
            _plataformaRepositoryMock = new();
        }

        [Fact(DisplayName = "Verificar se a plataforma não existe")]
        public async Task GetPlataformaNaoExiste()
        {
            // Arrange
            var query   = new GetPlataformaQuery { Id = 100 };
            var handler = new GetPlataformaQueryHandler(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock.Setup(rep => rep.GetByOrDefaultIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Plataforma?)null);

            // Assert
            await Assert.ThrowsAsync<ArgumentException>(async () => await handler.Handle(query, default));
        }

        [Fact(DisplayName = "Verificar se a plataforma existe")]
        public async Task GetPlataformaExiste()
        {
            // Arrange
            var result     = true;
            var query      = new GetPlataformaQuery { Id = 1 };
            var handler    = new GetPlataformaQueryHandler(_plataformaRepositoryMock.Object);
            var plataforma = new Plataforma("Playstation 5");

            _plataformaRepositoryMock
                .Setup(rep => rep.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(plataforma);

            // Act
            var hGenero = await handler.Handle(query, default);

            // Assert
            Assert.Equal(result, hGenero != null);
        }

        [Fact(DisplayName = "Adicionar plataforma não válida com título existente.")]
        public async Task AddPlataformaComTituloExistente()
        {
            // Arrange
            var plataforma = new AddPlataformaCommand { Titulo = "Playstation 5" };
            var validator  = new AddPlataformaValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
              .ReturnsAsync(true);

            // Assert
            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Já existe uma plataforma com esse título.", erros);
        }

        [Fact(DisplayName = "Adicionar plataforma não válida sem título.")]
        public async Task AddPlataformaSemTitulo()
        {
            // Arrange
            var plataforma = new AddPlataformaCommand { Titulo = "" };
            var validator  = new AddPlataformaValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
            .ReturnsAsync(false);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o titulo da plataforma.", erros);
        }

        [Fact(DisplayName = "Adicionar plataforma válida.")]
        public async Task AddPlataformaValido()
        {
            // Arrange
            var plataforma = new AddPlataformaCommand { Titulo = "Playstation 5" };
            var validator  = new AddPlataformaValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
             .ReturnsAsync(false);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Adicionar plataforma.")]
        public async Task AddPlataforma()
        {
            // Arrange
            var plataforma  = new AddPlataformaCommand { Titulo = "Playstation 5" };
            var handler     = new AddPlataformaCommandHandler(_plataformaRepositoryMock.Object);
            var oPlataforma = new Plataforma("Playstation 5");

            // Act
            _plataformaRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Plataforma>()))
                .ReturnsAsync(oPlataforma);

            var plataformaDto = await handler.Handle(plataforma, default);

            // Assert
            plataformaDto.Titulo.Should().Be(plataforma.Titulo);
        }

        [Fact(DisplayName = "Editar plataforma não válida sem titulo.")]
        public async Task EditPlataformaSemTitulo()
        {
            // Arrange
            var plataforma = new EditPlataformaCommand { Titulo = "" };
            var validator  = new EditPlataformaValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock
              .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync(new Plataforma("Playstation 5"));

            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
                .ReturnsAsync(false);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Informe o titulo da plataforma.", erros);
        }
        [Fact(DisplayName = "Editar plataforma não válida com título existente.")]
        public async Task EditPlataformaComTituloExistente()
        {
            // Arrange
            var plataforma = new EditPlataformaCommand { Titulo = "" };
            var validator  = new EditPlataformaValidator(_plataformaRepositoryMock.Object);

            // Act 
            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("Playstation 5"));

            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
            .ReturnsAsync(true);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("Já existe uma plataforma com esse título.", erros);
        }

        [Fact(DisplayName = "Editar plataforma válida.")]
        public async Task EditPlataformaValido()
        {
            // Arrange
            var plataforma = new EditPlataformaCommand { Titulo = "Playstation 5" };
            var validator  = new EditPlataformaValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(new Plataforma("Playstation 5"));

            _plataformaRepositoryMock.Setup(r => r.ExistsByAsync(It.IsAny<Expression<Func<Plataforma, bool>>>()))
            .ReturnsAsync(false);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Editar plataforma.")]
        public async Task EditPlataforma()
        {
            // Arrange
            var command    = new EditPlataformaCommand { Titulo = "Playstation 5" };
            var handler    = new EditPlataformaCommandHandler(_plataformaRepositoryMock.Object);
            var plataforma = new Plataforma("Playstation 5");

            // Act
            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(plataforma);

            _plataformaRepositoryMock
                .Setup(repo => repo.UpdateAsync(It.IsAny<Plataforma>()));

            var plataformaDto = await handler.Handle(command, default);

            // Assert
            plataformaDto.Titulo.Should().Be(plataforma.Titulo);
        }

        [Fact(DisplayName = "Deletar plataforma inexistente.")]
        public async Task DeletePlataformaInexistente()
        {
            // Arrange
            var plataforma = new DeletePlataformaCommand { Id = 1 };
            var validator  = new DeletePlataformaValidator(_plataformaRepositoryMock.Object);

            // Act
            _plataformaRepositoryMock
                .Setup(repo => repo.GetByOrDefaultIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Plataforma?)null);

            var result = await validator.ValidateAsync(plataforma);

            // Assert
            Assert.False(result.IsValid);
            var erros = result.Errors.Select(e => e.ErrorMessage).ToList();
            Assert.Contains("O id da plataforma informado não foi encontrado.", erros);
        }

        [Fact(DisplayName = "Deletar plataforma existente.")]
        public async Task DeletePlataformaExistente()
        {
            // Arrange
            var command    = new DeletePlataformaCommand { Id = 1 };
            var validator  = new DeletePlataformaValidator(_plataformaRepositoryMock.Object);
            var plataforma = new Plataforma("Playstation 5");

            // Act
            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(plataforma);

            var result = await validator.ValidateAsync(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Deletar plataforma.")]
        public async Task DeletePlataforma()
        {
            // Arrange
            var command    = new DeletePlataformaCommand { Id = 1 };
            var handler    = new DeletePlataformaCommandHandler(_plataformaRepositoryMock.Object);
            var plataforma = new Plataforma("Playstation 5");

            // Act
            _plataformaRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(plataforma);

            _plataformaRepositoryMock
                .Setup(repo => repo.DeleteAsync(It.IsAny<int>()));

            var deletado = await handler.Handle(command, default);

            // Assert
            Assert.True(deletado);
        }
    }
}
